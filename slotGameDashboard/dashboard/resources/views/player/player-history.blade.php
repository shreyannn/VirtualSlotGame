@extends('layouts.app') {{-- Assuming you have a layout file named app.blade.php --}}

@section('content')
<div class="container mt-5">
    <h1 class="text-3xl font-semibold mb-4">Player Activity</h1>

    {{-- Check if there are any transactions --}}
    @if ($transactions->isEmpty())
        <p class="text-muted">You have no activity history.</p>
    @else
        <div class="table-responsive">
           
    <table class="min-w-full table-auto">
        <thead class="bg-gray-700 text-white">
                    <tr>
                        <th class="px-6 py-3 text-sm font-medium" >#</th>
                        <th class="px-6 py-3 text-sm font-medium" >Old Balance</th>
                        <th class="px-6 py-3 text-sm font-medium" >New Balance</th>
                        <th class="px-6 py-3 text-sm font-medium" >Spin Count</th>
                        <th class="px-6 py-3 text-sm font-medium" >Bet Amount</th>
                        <th class="px-6 py-3 text-sm font-medium" >Win Amount</th>
                        <th class="px-6 py-3 text-sm font-medium" >RTP</th>
                        <th class="px-6 py-3 text-sm font-medium">Date</th>
                       
                    </tr>
                </thead>
                <tbody class="text-center">
                    @foreach ($transactions as $index => $transaction)
                        <tr>
                            <td class="px-6 py-4" >{{ $index + 1 }}</td>
                            <td class="px-6 py-4" >{{ number_format($transaction->old_balance, 2) }}</td>
                            <td class="px-6 py-4" >{{ number_format($transaction->new_balance, 2) }}</td>
                            <td class="px-6 py-4" >{{ $transaction->spin_count }}</td>
                            <td class="px-6 py-4" >{{ number_format($transaction->bet, 2) }}</td>
                            <td class="px-6 py-4" >{{ number_format($transaction->win, 2) }}</td>
                            <td class="px-6 py-4" >{{ number_format($transaction->RTP, 2) }}%</td>
                            <td class="px-6 py-4">{{ $transaction->created_at }}</td>
                        </tr>
                    @endforeach
                </tbody>
            </table>
        </div>
    @endif
</div>
@endsection
