<?php

namespace App\Http\Controllers;

use App\Models\PlayerActivity;
use Illuminate\Support\Facades\Auth;

class PlayerActivityController extends Controller
{
    /**
     * Display the player's activity history.
     *
     * @return \Illuminate\View\View
     */
    public function PlayerHistory()
    {
        // Get the logged-in user
        $user = Auth::user();

        // Fetch player activities for the logged-in user, ordered by creation date (most recent first)
        $transactions = PlayerActivity::where('user_id', $user->id)
            ->orderBy('created_at', 'desc')
            ->get();

        // Return the view with the transactions data
        return view('player.player-history', compact('transactions'));
    }
}
