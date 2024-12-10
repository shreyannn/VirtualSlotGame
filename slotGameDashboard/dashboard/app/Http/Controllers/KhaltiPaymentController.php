<?php

namespace App\Http\Controllers;

use App\Models\Transaction;
use App\Models\User;
use App\Models\UserBalance;
use Illuminate\Http\Request;
use Validator;
use Session;

class KhaltiPaymentController extends Controller
{
    public function initiatePayment(Request $request)
    {
       
        // Validation rules for the incoming request
        $validator = Validator::make($request->all(), [
            'inputAmount4' => 'required|numeric|min:1',
            'inputPurchasedOrderId4' => 'required|string',
            'inputPurchasedOrderName' => 'required|string',
            'inputName' => 'required|string',
            'inputEmail' => 'required|email',
            'inputPhone' => 'required|numeric',
        ]);

        if ($validator->fails()) {
            // Redirect back with validation errors
            return redirect()->route('checkout')->withErrors($validator)->withInput();
        }

        // If validation passes, process the payment
        $amount = $request->input('inputAmount4');  // Convert to paisa
        $purchase_order_id = $request->input('inputPurchasedOrderId4');
        $purchase_order_name = $request->input('inputPurchasedOrderName');
        $name = $request->input('inputName');
        $email = $request->input('inputEmail');
        $phone = $request->input('inputPhone');

        // Prepare POST fields for the Khalti API
     
        $postFields = [
            "return_url" => route('payment.response'), // Callback route
            "website_url" => url('/'), // Your website URL
            "amount" => $amount,
            "purchase_order_id" => $purchase_order_id,
            "purchase_order_name" => $purchase_order_name,
            "customer_info" => [
                "name" => $name,
                "email" => $email,
                "phone" => $phone
            ]
        ];

        // $postFields = [
        //     "return_url" => "http://localhost:8000/payment-response",
        //     "website_url" => "http://localhost:8000",
        //     "amount" => "1000",
        //     "purchase_order_id" => "1255",
        //     "purchase_order_name" => "Order-8716",
        //     "customer_info" => [
        //         "name" => "User80",
        //         "email" => "user95@example.com",
        //         "phone" => "9891338937"
        //     ]
        // ];
        
        // dd($postFields);
        // Call the Khalti API
        $response = $this->callKhaltiApi($postFields);
        // dd($response);

        // Handle the response from Khalti API
        if ($response == 'error') {
            // Handle the error, log or show a message
            return redirect()->route('checkout')->with('error', 'Payment initiation failed: ' . $response['error']);
        }

        // If payment URL exists, redirect to Khalti payment page
        if (isset($response['payment_url'])) {
            return redirect($response['payment_url']);
        } else {
            return redirect()->route('checkout')->with('error', 'Unexpected response from Khalti.');
        }
    }

    // Function to make the CURL request to Khalti API
    private function callKhaltiApi($postFields)
    {
       
        $jsonData = json_encode($postFields);
        $curl = curl_init();

        curl_setopt_array($curl, [
            CURLOPT_URL => 'https://a.khalti.com/api/v2/epayment/initiate/',
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_ENCODING => '',
            CURLOPT_MAXREDIRS => 10,
            CURLOPT_TIMEOUT => 0,
            CURLOPT_FOLLOWLOCATION => true,
            CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
            CURLOPT_CUSTOMREQUEST => 'POST',
            CURLOPT_POSTFIELDS => $jsonData,
            CURLOPT_HTTPHEADER => [
                'Authorization: key live_secret_key_68791341fdd94846a146f0457ff7b455',
                'Content-Type: application/json',
            ],
        ]);

        $response = curl_exec($curl);

        if (curl_errno($curl)) {
            return ['error' => curl_error($curl)];
        }

        curl_close($curl);
        // Decode the response to an array
        return json_decode($response, true);
    }

    // Handle the response from Khalti after payment (you need to create this route)

    public function paymentResponse(Request $request)
    {
        if ($request['status'] == "Completed") {
            $paymentId = $request['pidx'];
            $amount = $request['total_amount'];  
            $orderId = $request['purchase_order_id']; 
            $userId = auth()->user()->id;  

            $paymentSuccess = true; 
            if ($paymentSuccess) {
                $user = User::find($userId);

                if ($user) {

                    Transaction::create([
                        'user_id' => $userId,
                        'order_id' => $orderId,
                        'amount' => $amount,
                        'transaction_id' => $paymentId,
                        'status' => 'completed',
                    ]);

                    $userBalance = UserBalance::where('user_id', $userId)->first();

                    if ($userBalance) {
                        $userBalance->balance += $amount;
                        $userBalance->save();
                    } else {
                        UserBalance::create([
                            'user_id' => $userId,
                            'balance' => $amount,
                        ]);
                    }

                    return view('player.buy-coins')->with([
                        'message' => 'Payment successful! Your balance has been updated.',
                        'balance' => $user->balance,  
                    ]);
                } else {
                    return view('player.buy-coins')->with([
                        'message' => 'User not found.',
                    ]);
                }
            }
        }
        // Step 4: If payment verification fails
        return view('player.buy-coins')->with([
            'message' => 'Payment failed, please try again.',
        ]);
    }
}
