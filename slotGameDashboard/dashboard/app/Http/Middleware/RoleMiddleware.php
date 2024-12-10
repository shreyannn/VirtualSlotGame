<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class RoleMiddleware
{
    public function handle(Request $request, Closure $next, $role)
    {
        if (Auth::check()) {
            $user = Auth::user();

            if ($role == 'admin' && $user->role != 'admin') {
                return redirect('/')->with('error', 'Access Denied');
            }

            if ($role == 'player' && $user->role != 'player') {
                return redirect('/')->with('error', 'Access Denied');
            }

            return $next($request);
        }

        // If the user is not logged in, proceed to login page
        return redirect()->route('login');
    }
}
