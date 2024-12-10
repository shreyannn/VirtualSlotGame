@extends('layouts.app')

@section('content')
    <div class="container mx-auto">
        @if (Auth::user() && Auth::user()->role == 'admin')
            <h2 class="text-3xl font-semibold mb-6 text-center text-white">Welcome, Admin!</h2>

            <div class="mb-6">
                <h3 class="text-xl font-medium text-gray-300">Total Users: <span
                        class="font-semibold text-green-500">{{ $userCount }}</span></h3>
            </div>

            <!-- Users Table -->
            <div class="overflow-x-auto bg-gray-800 rounded-lg shadow-lg">
                <table class="min-w-full table-auto text-left">
                    <thead class="bg-gray-700 text-white">
                        <tr>
                            <th class="px-6 py-3 text-sm font-medium">Name</th>
                            <th class="px-6 py-3 text-sm font-medium">Email</th>
                            <th class="px-6 py-3 text-sm font-medium">Role</th>
                            <th class="px-6 py-3 text-sm font-medium">Status</th>
                            <th class="px-6 py-3 text-sm font-medium">Action</th>
                        </tr>
                    </thead>
                    <tbody class="text-gray-300">
                        @foreach ($users as $user)
                            <tr class="border-b border-gray-700">
                                <td class="px-6 py-4">{{ $user->name }}</td>
                                <td class="px-6 py-4">{{ $user->email }}</td>
                                <td class="px-6 py-4">{{ ucfirst($user->role) }}</td>
                                <td class="px-6 py-4">
                                    @if ($user->isBanned())
                                        <span class="text-red-500">Banned</span>
                                    @else
                                        <span class="text-green-500">Active</span>
                                    @endif
                                </td>
                                <td class="px-6 py-4">
                                    @if ($user->banned)
                                        <form action="{{ route('admin.unban', $user->id) }}" method="POST" class="inline">
                                            @csrf
                                            @method('PUT')
                                            <button type="submit"
                                                class="text-sm px-4 py-2 bg-green-500 hover:bg-green-700 text-white rounded-md">Unban</button>
                                        </form>
                                    @else
                                        <form action="{{ route('admin.ban', $user->id) }}" method="POST" class="inline">
                                            @csrf
                                            @method('PUT')
                                            <button type="submit"
                                                class="text-sm px-4 py-2 bg-red-500 hover:bg-red-700 text-white rounded-md">Ban</button>
                                        </form>
                                    @endif
                                </td>

                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
        @endif
    </div>
@endsection
@section('layout.toaster')
@endsection
