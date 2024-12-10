@extends('layouts.app')

@section('content')
    <div class="bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 p-8 rounded-lg shadow-xl max-w-4xl mx-auto">
        <h2 class="text-4xl font-bold text-white mb-6 text-center">Welcome, {{ Auth::user()->name }}!</h2>
        
        <div class="bg-white p-6 rounded-lg shadow-lg mb-6">
            <h1 class="text-3xl font-semibold text-gray-800 mb-2">Your Dashboard</h1>
            <p class="text-lg text-gray-600">Your current balance: <strong class="text-green-500 text-2xl">{{ $balance }} coins</strong></p>
        </div>

        <div class="flex justify-center gap-6 mt-6">
            <!-- Add Coins Button -->
            <button class="bg-gradient-to-r from-yellow-400 to-red-500 text-white py-3 px-6 rounded-full shadow-lg transform transition duration-300 ease-in-out hover:scale-105 focus:outline-none">
                Add Coins
            </button>
        
            <!-- Redeem Coins Button -->
            <button class="bg-gradient-to-r from-green-400 to-blue-500 text-white py-3 px-6 rounded-full shadow-lg transform transition duration-300 ease-in-out hover:scale-105 focus:outline-none">
                Redeem Coins
            </button>
        </div>
        
    </div>
@endsection
