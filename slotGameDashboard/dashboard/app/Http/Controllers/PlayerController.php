<?php

namespace App\Http\Controllers;

use App\Models\UserBalance;
use Illuminate\Http\Request;

class PlayerController extends Controller
{
    public function index()
    {
        // Get the authenticated user
        $user = auth()->user();
    
        $userBalance = UserBalance::where('user_id', $user->id)->first();
    
        // Check if the balance record exists
        if ($userBalance) {
            $balance = $userBalance->balance;  // Get the balance
        } else {
            $balance = 0;  // Default value if no balance record is found
        }
    
        // Pass the balance to the view
        return view('player.dashboard', compact('balance'));
    }
}
