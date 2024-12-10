<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Dashboard</title>
    @vite('resources/css/app.css') <!-- Ensure Tailwind CSS is included -->
</head>
<body class="bg-gradient-to-r from-blue-500 via-purple-600 to-pink-500 text-white font-sans leading-relaxed">

    <!-- Main Wrapper -->
    <div class="flex min-h-screen">

        <!-- Sidebar for Admin and Player -->
        <div class="w-64 bg-gradient-to-b from-gray-900 to-gray-700 p-6 space-y-6 flex-shrink-0">

            <!-- Header -->
            <div class="flex items-center justify-between">
                <h2 class="text-2xl font-semibold text-gray-300">Dashboard</h2>
                <button class="lg:hidden text-gray-300" id="sidebar-toggle">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" class="w-6 h-6">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"></path>
                    </svg>
                </button>
            </div>

            <!-- Sidebar Menu (Admin) -->
            @if(Auth::user() && Auth::user()->role == 'admin')
                <div>
                    <ul class="space-y-4">
                        <li>
                            <a href="{{ route('admin.dashboard') }}" class="block px-4 py-2 rounded-md text-gray-300 hover:bg-gradient-to-r hover:from-yellow-400 hover:to-red-500 hover:text-white transition duration-200">
                                <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 inline-block mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 5h18M3 10h18M3 15h18M3 20h18"></path>
                                </svg>
                                Dashboard
                            </a>
                        </li>
                        <li>
                            <a href="{{ route('admin.transaction-history') }}" class="block px-4 py-2 rounded-md text-gray-300 hover:bg-gradient-to-r hover:from-yellow-400 hover:to-red-500 hover:text-white transition duration-200">
                                <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 inline-block mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 10l5 5 5-5H7z"></path>
                                </svg>
                                Transactions
                            </a>
                        </li>
                    </ul>
                </div>
            @endif

            <!-- Sidebar Menu (Player) -->
            @if(Auth::user() && Auth::user()->role == 'player')
                <div>
                    <ul class="space-y-4">
                        <li>
                            <a href="{{ route('player.dashboard') }}" class="block px-4 py-2 rounded-md text-gray-300 hover:bg-gradient-to-r hover:from-blue-400 hover:to-green-500 hover:text-white transition duration-200">
                                <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 inline-block mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h14M12 5l7 7-7 7"></path>
                                </svg>
                                Home
                            </a>
                        </li>
                        <li>
                            <a href="{{ route('buy.coins') }}" class="block px-4 py-2 rounded-md text-gray-300 hover:bg-gradient-to-r hover:from-blue-400 hover:to-green-500 hover:text-white transition duration-200">
                                <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 inline-block mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 9h8M8 12h8M8 15h8"></path>
                                </svg>
                                Buy Coins
                            </a>
                        </li>
                        <li>
                            <a href="{{ route('player.transaction-history') }}" class="block px-4 py-2 rounded-md text-gray-300 hover:bg-gradient-to-r hover:from-blue-400 hover:to-green-500 hover:text-white transition duration-200">
                                <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 inline-block mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 10l5 5 5-5H7z"></path>
                                </svg>
                                Transaction History
                            </a>
                        </li>

                        <li>
                            <a href="{{ route('player.player-history') }}" class="block px-4 py-2 rounded-md text-gray-300 hover:bg-gradient-to-r hover:from-blue-400 hover:to-green-500 hover:text-white transition duration-200">
                                <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 inline-block mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 10l5 5 5-5H7z"></path>
                                </svg>
                                Player Activity
                            </a>
                        </li>
                    </ul>
                </div>
            @endif

            <!-- Logout Button -->
            <form action="{{ route('logout') }}" method="POST" class="mt-8 px-4 py-2">
                @csrf
                <button type="submit" class="w-full bg-red-600 text-white py-2 rounded-md text-center hover:bg-red-700 focus:outline-none transition duration-200">Logout</button>
            </form>
        </div>

        <!-- Main Content Area -->
        <div class="flex-1 bg-gray-800 p-8 overflow-y-auto">
            @yield('content')
        </div>

    </div>

    <!-- Sidebar Toggle Script (Mobile) -->
    <script>
        const sidebarToggle = document.getElementById('sidebar-toggle');
        const sidebar = document.querySelector('.w-64');
        
        sidebarToggle.addEventListener('click', () => {
            sidebar.classList.toggle('hidden');
        });
    </script>

</body>
</html>
