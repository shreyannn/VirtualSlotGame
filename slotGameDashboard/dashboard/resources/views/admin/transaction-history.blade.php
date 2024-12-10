<!-- resources/views/admin/transaction-history.blade.php -->

@extends('layouts.app')

@section('content')
<div class="container mx-auto">
    <h2 class="text-3xl font-semibold mb-4">All Users Transaction History</h2>

    <table class="min-w-full table-auto">
        <thead class="bg-gray-700 text-white">
            <tr>
                <th class="px-6 py-3 text-sm font-medium">User</th>
                <th class="px-6 py-3 text-sm font-medium">Amount</th>
                <th class="px-6 py-3 text-sm font-medium">Type</th>
                <th class="px-6 py-3 text-sm font-medium">Payment Method</th>
                <th class="px-6 py-3 text-sm font-medium">Status</th>
                <th class="px-6 py-3 text-sm font-medium">Date</th>
            </tr>
        </thead>
        <tbody class="text-gray-300">
            @foreach($transactions as $transaction)
                <tr class="border-b border-gray-700">
                    <td class="px-6 py-4">{{ $transaction->user->name }}</td>
                    <td class="px-6 py-4">{{ $transaction->amount }}</td>
                    <td class="px-6 py-4">{{ ucfirst($transaction->type) }}</td>
                    <td class="px-6 py-4">{{ $transaction->payment_method }}</td>
                    <td class="px-6 py-4">{{ ucfirst($transaction->status) }}</td>
                    <td class="px-6 py-4">{{ $transaction->created_at->format('Y-m-d H:i') }}</td>
                </tr>
            @endforeach
        </tbody>
    </table>
</div>
@endsection
