<?php

namespace Database\Seeders;

use App\Models\User;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Hash;

class AdminUserSeeder extends Seeder
{
    public function run()
    {
        User::create([
            'name' => 'Admin User',
            'email' => 'admin@example.com',
            'password' => Hash::make('password'), // Replace with a secure password
            'role' => 'admin',
        ]);
        User::create([
            'name' => 'User',
            'email' => 'user@example.com',
            'password' => Hash::make('password'), // Replace with a secure password
            'role' => 'player',
        ]);
    }
}
