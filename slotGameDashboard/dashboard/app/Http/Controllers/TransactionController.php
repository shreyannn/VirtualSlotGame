<?php

namespace App\Http\Controllers;

use App\Models\Transaction;
use App\Models\UserBalance;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class TransactionController extends Controller
{
   
    // Show transaction history for the logged-in user
    public function transactionHistory()
    {
        $user = Auth::user();
        $transactions = Transaction::where('user_id', $user->id)->get();

        return view('player.transaction-history', compact('transactions'));
    }

    // Show all transactions for the admin
    public function adminTransactionHistory()
    {
        $transactions = Transaction::all();
        return view('admin.transaction-history', compact('transactions'));
    }

    // Show Buy Coins form
    public function showBuyCoinsForm()
    {
        return view('player.buy-coins');
    }
}
