<nav x-data="{ open: false }" class="bg-gray-800 text-white dark:bg-gray-900">
    <!-- Sidebar -->
    <div :class="{'block': open, 'hidden': ! open}" class="sm:hidden fixed inset-0 z-50 bg-black bg-opacity-50"></div>

    <div class="flex">
        <!-- Sidebar for Desktop -->
        <div class="bg-gray-800 text-white w-64 min-h-screen flex flex-col fixed">
            <div class="flex items-center justify-center p-4">
                <x-application-logo class="h-9 w-auto fill-current text-white" />
            </div>
            <div class="flex-grow">
                <ul class="space-y-4 p-4">
                    <li>
                        <a href="{{ route('admin.dashboard') }}" class="text-gray-200 hover:text-white">Dashboard</a>
                    </li>
                    <li>
                        <a href="{{ route('profile.edit') }}" class="text-gray-200 hover:text-white">Profile</a>
                    </li>
                    <li>
                        <form method="POST" action="{{ route('logout') }}">
                            @csrf
                            <button type="submit" class="text-gray-200 hover:text-white w-full text-left">Log Out</button>
                        </form>
                    </li>
                </ul>
            </div>
        </div>

        <!-- Main Content -->
        <div class="flex-1 ml-64 bg-gray-100 dark:bg-gray-800">
            <div class="bg-gray-800 text-white p-4 flex justify-between items-center">
                <!-- Hamburger Menu for Mobile -->
                <button @click="open = ! open" class="sm:hidden inline-flex items-center p-2 rounded-md text-white hover:bg-gray-700">
                    <svg class="h-6 w-6" stroke="currentColor" fill="none" viewBox="0 0 24 24">
                        <path :class="{'hidden': open, 'inline-flex': ! open }" class="inline-flex" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16" />
                        <path :class="{'hidden': ! open, 'inline-flex': open }" class="hidden" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
                    </svg>
                </button>

                <!-- User Profile Dropdown -->
                <div class="flex items-center space-x-4">
                    <div>{{ Auth::user()->name }}</div>
                    <x-dropdown align="right" width="48">
                        <x-slot name="trigger">
                            <button class="flex items-center space-x-2 text-white hover:text-gray-300">
                                <svg class="w-6 h-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h14M5 16h14M5 8h14" />
                                </svg>
                            </button>
                        </x-slot>

                        <x-slot name="content">
                            <x-dropdown-link :href="route('profile.edit')">
                                {{ __('Profile') }}
                            </x-dropdown-link>

                            <form method="POST" action="{{ route('logout') }}">
                                @csrf
                                <x-dropdown-link :href="route('logout')"
                                        onclick="event.preventDefault(); this.closest('form').submit();">
                                    {{ __('Log Out') }}
                                </x-dropdown-link>
                            </form>
                        </x-slot>
                    </x-dropdown>
                </div>
            </div>

            <!-- Responsive Sidebar Menu -->
            <div :class="{'block': open, 'hidden': ! open}" class="sm:hidden fixed inset-0 bg-gray-800 bg-opacity-75 z-50">
                <div class="bg-gray-800 text-white w-64 min-h-screen p-4">
                    <ul class="space-y-4">
                        <li>
                            <a href="{{ route('admin.dashboard') }}" class="block text-gray-200 hover:text-white">Dashboard</a>
                        </li>
                        <li>
                            <a href="{{ route('profile.edit') }}" class="block text-gray-200 hover:text-white">Profile</a>
                        </li>
                        <li>
                            <form method="POST" action="{{ route('logout') }}">
                                @csrf
                                <button type="submit" class="block text-gray-200 hover:text-white w-full text-left">Log Out</button>
                            </form>
                        </li>
                    </ul>
                </div>
            </div>

            <!-- Main Content Area -->
            <div class="p-6">
                @yield('content')
            </div>
        </div>
    </div>
</nav>
