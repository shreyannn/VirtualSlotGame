@extends('layouts.app')

@section('content')
    <div class="max-w-lg mx-auto bg-gradient-to-r from-purple-500 via-pink-500 to-red-500 p-6 rounded-lg shadow-lg">
        <h1 class="text-3xl font-bold text-center text-white mb-6 animate__animated animate__fadeIn">Buy Coins</h1>

        <!-- Coin Animation -->
        <div class="flex justify-center mb-6">
            <svg class="animate-spin-slow transform" fill="#ffffff" height="80" viewBox="0 0 64 64" xmlns="http://www.w3.org/2000/svg">
                <!-- Add your SVG here -->
            </svg>
        </div>

        <!-- Khalti Payment Form -->
        <form action="{{ route('initiate.payment') }}" method="POST" class="mt-6">
            @csrf
            <!-- Select Coin Amount -->
            <label for="inputAmount4" class="block text-white mb-2 text-lg">Select Amount</label>
            <input type="number" name="inputAmount4" id="inputAmount4" min="10" required
                class="w-full p-3 mb-4 rounded-md bg-gray-800 text-white border border-gray-700 focus:ring-2 focus:ring-indigo-600 focus:outline-none transition duration-300 ease-in-out"
                placeholder="Amount">

            <!-- Hidden Inputs with Random Values -->
            <input type="hidden" name="inputPurchasedOrderId4" value="{{ rand(1000, 9999) }}">
            <input type="hidden" name="inputPurchasedOrderName" value="Order-{{ rand(1000, 9999) }}">
            <input type="hidden" name="inputName" value="User{{ rand(1, 100) }}">
            <input type="hidden" name="inputEmail" value="user{{ rand(1, 100) }}@example.com">
            <input type="hidden" name="inputPhone" value="{{ rand(9800000000, 9899999999) }}">
            
            <!-- Khalti Payment Button -->
            <div class="mt-6">
                <button type="submit"
                    class="w-full bg-gradient-to-r from-yellow-400 to-red-500 text-white py-3 rounded-md shadow-md hover:scale-105 focus:outline-none transform hover:bg-gradient-to-l transition duration-300 ease-in-out">
                    Proceed to Khalti Payment
                </button>
            </div>
        </form>

        <div class="mt-4 text-center">
            <p class="text-white">By proceeding, you will be redirected to Khalti for payment.</p>
        </div>

        <!-- Success Modal -->
        @if(isset($paymentStatus) && $paymentStatus === 'success')
            <div id="successModal" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
                <div class="bg-white p-6 rounded-lg shadow-lg">
                    <h2 class="text-2xl font-semibold text-green-500">Payment Successful!</h2>
                    <p class="text-gray-700">Your payment was successfully processed. Your coins have been added.</p>
                    <button id="closeSuccessModal" class="mt-4 bg-green-500 text-white py-2 px-4 rounded-md">Close</button>
                </div>
            </div>
        @endif

        <!-- Failure Modal -->
        @if(isset($paymentStatus) && $paymentStatus === 'failure')
            <div id="failureModal" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
                <div class="bg-white p-6 rounded-lg shadow-lg">
                    <h2 class="text-2xl font-semibold text-red-500">Payment Failed!</h2>
                    <p class="text-gray-700">There was an issue with your payment. Please try again.</p>
                    <button id="closeFailureModal" class="mt-4 bg-red-500 text-white py-2 px-4 rounded-md">Close</button>
                </div>
            </div>
        @endif
    </div>

    <style>
        /* Coin SVG Animation */
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }

        .animate-spin-slow {
            animation: spin 4s linear infinite;
        }

        /* Button Hover Animation */
        .transform:hover {
            transform: scale(1.05);
        }

        /* Add some fun animations */
        @keyframes fadeIn {
            0% { opacity: 0; }
            100% { opacity: 1; }
        }

        .animate__animated {
            animation: fadeIn 2s ease-out forwards;
        }

        /* Enhance button hover effect */
        .hover\:bg-gradient-to-l:hover {
            background: linear-gradient(to left, #fbbf24, #ef4444);
        }
    </style>

    <script>
        // Close success modal
        document.getElementById('closeSuccessModal')?.addEventListener('click', function () {
            document.getElementById('successModal').style.display = 'none';
        });

        // Close failure modal
        document.getElementById('closeFailureModal')?.addEventListener('click', function () {
            document.getElementById('failureModal').style.display = 'none';
        });
    </script>
@endsection
