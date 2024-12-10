<?php

// app/Http/Controllers/Admin/AdminController.php

namespace App\Http\Controllers\Admin;

use App\Models\User;
use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Auth;

class AdminController extends Controller
{
    /**
     * Apply the middleware that ensures the user is an admin.
     */
    public function __construct()
    {
        $this->middleware('role:admin');
    }

    /**
     * Display the total number of users.
     */
    public function showTotalUsers()
    {
        $userCount = User::where('role', '!=','admin')->count();
        $users = User::where('role', '!=','admin')->get(); // Get all users to display for management
        return view('admin.dashboard', compact('userCount', 'users'));
    }

    /**
     * Ban a user.
     */
    public function banUser($id)
    {
        $user = User::findOrFail($id);
        $user->ban();
        return redirect()->route(route: 'admin.dashboard')->with('status', 'User banned!');
    }

    /**
     * Unban a user.
     */
    public function unbanUser($id)
    {
        $user = User::findOrFail($id);
        $user->unban();

        return redirect()->route('admin.dashboard')->with('status', 'User unbanned!');
    }
}
