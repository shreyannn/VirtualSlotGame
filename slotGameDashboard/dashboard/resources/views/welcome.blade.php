<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Lucky Spins - Premium Slot Experience</title>
    @vite('resources/css/app.css')
    <script src="https://cdnjs.cloudflare.com/ajax/libs/alpinejs/2.8.2/alpine.js" defer></script>
</head>
<body class="bg-gray-900 text-white font-sans">
    <!-- Navigation -->
    <nav class="fixed w-full z-50 bg-gray-900/80 backdrop-blur-md">
        <div class="container mx-auto px-4 py-3">
            <div class="flex justify-between items-center">
                <div class="text-2xl font-bold text-yellow-500">Lucky Spins</div>
                <div class="space-x-6">
                    <a href="#features" class="text-gray-300 hover:text-yellow-500 transition">Features</a>
                    <a href="#games" class="text-gray-300 hover:text-yellow-500 transition">Games</a>
                    <a href="#testimonials" class="text-gray-300 hover:text-yellow-500 transition">Testimonials</a>
                </div>
            </div>
        </div>
    </nav>

    <!-- Hero Section -->
    <section class="relative min-h-screen flex items-center justify-center bg-gradient-to-r from-purple-900 via-blue-900 to-purple-900">
        <div class="absolute inset-0 overflow-hidden">
            <div class="absolute inset-0 bg-gradient-to-b from-transparent via-purple-900/50 to-gray-900"></div>
            <img src="/api/placeholder/1920/1080" alt="Slot machine background" class="w-full h-full object-cover opacity-30">
        </div>
        <div class="relative z-10 container mx-auto px-4 text-center">
            <h1 class="text-5xl md:text-7xl font-extrabold mb-6 leading-tight bg-clip-text text-transparent bg-gradient-to-r from-yellow-400 via-red-400 to-pink-400">
                Experience the Ultimate<br>Slot Adventure
            </h1>
            <p class="text-xl md:text-2xl text-gray-300 mb-8 max-w-2xl mx-auto">
                Dive into a world of excitement with over 1000+ premium slots, daily bonuses, and life-changing jackpots!
            </p>
            <div class="flex flex-col sm:flex-row justify-center gap-4 sm:gap-6">
                <a href="{{ route('login') }}" class="group relative px-8 py-4 bg-gradient-to-r from-yellow-500 to-red-500 rounded-full overflow-hidden">
                    <span class="relative z-10 text-xl font-semibold">Start Playing Now</span>
                    <div class="absolute inset-0 bg-gradient-to-r from-yellow-400 to-red-400 transform scale-x-0 group-hover:scale-x-100 transition-transform origin-left"></div>
                </a>
                <a href="{{ route('register') }}" class="group relative px-8 py-4 bg-gradient-to-r from-purple-500 to-blue-500 rounded-full overflow-hidden">
                    <span class="relative z-10 text-xl font-semibold">Create Account</span>
                    <div class="absolute inset-0 bg-gradient-to-r from-purple-400 to-blue-400 transform scale-x-0 group-hover:scale-x-100 transition-transform origin-left"></div>
                </a>
            </div>
        </div>
    </section>

    <!-- Features Section -->
    <section id="features" class="py-20 bg-gray-900">
        <div class="container mx-auto px-4">
            <h2 class="text-4xl font-bold text-center mb-16 bg-clip-text text-transparent bg-gradient-to-r from-yellow-400 to-red-400">
                Why Choose Lucky Spins?
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
                <div class="group bg-gray-800 rounded-2xl p-6 transform transition-all duration-300 hover:-translate-y-2">
                    <div class="mb-6 relative w-16 h-16 mx-auto">
                        <div class="absolute inset-0 bg-yellow-400 rounded-xl transform rotate-6 group-hover:rotate-12 transition-transform"></div>
                        <div class="absolute inset-0 bg-gray-800 rounded-xl flex items-center justify-center">
                            <img src="/api/placeholder/64/64" alt="Trophy" class="w-8 h-8">
                        </div>
                    </div>
                    <h3 class="text-xl font-semibold text-yellow-400 mb-4 text-center">Massive Jackpots</h3>
                    <p class="text-gray-400 text-center">Win life-changing prizes with our progressive jackpot system.</p>
                </div>

                <div class="group bg-gray-800 rounded-2xl p-6 transform transition-all duration-300 hover:-translate-y-2">
                    <div class="mb-6 relative w-16 h-16 mx-auto">
                        <div class="absolute inset-0 bg-blue-400 rounded-xl transform rotate-6 group-hover:rotate-12 transition-transform"></div>
                        <div class="absolute inset-0 bg-gray-800 rounded-xl flex items-center justify-center">
                            <img src="/api/placeholder/64/64" alt="Security" class="w-8 h-8">
                        </div>
                    </div>
                    <h3 class="text-xl font-semibold text-blue-400 mb-4 text-center">Secure Gaming</h3>
                    <p class="text-gray-400 text-center">Industry-leading security measures to protect your data and funds.</p>
                </div>

                <div class="group bg-gray-800 rounded-2xl p-6 transform transition-all duration-300 hover:-translate-y-2">
                    <div class="mb-6 relative w-16 h-16 mx-auto">
                        <div class="absolute inset-0 bg-green-400 rounded-xl transform rotate-6 group-hover:rotate-12 transition-transform"></div>
                        <div class="absolute inset-0 bg-gray-800 rounded-xl flex items-center justify-center">
                            <img src="/api/placeholder/64/64" alt="Support" class="w-8 h-8">
                        </div>
                    </div>
                    <h3 class="text-xl font-semibold text-green-400 mb-4 text-center">24/7 Support</h3>
                    <p class="text-gray-400 text-center">Round-the-clock customer support for all your gaming needs.</p>
                </div>
            </div>
        </div>
    </section>

    <!-- Popular Games -->
    <section id="games" class="py-20 bg-gray-800">
        <div class="container mx-auto px-4">
            <h2 class="text-4xl font-bold text-center mb-16 bg-clip-text text-transparent bg-gradient-to-r from-purple-400 to-blue-400">
                Popular Games
            </h2>
            <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
                <div class="group relative overflow-hidden rounded-xl">
                    <img src="/api/placeholder/300/400" alt="Game 1" class="w-full h-72 object-cover transform group-hover:scale-110 transition-transform duration-300">
                    <div class="absolute inset-0 bg-gradient-to-t from-black/80 to-transparent flex items-end p-6">
                        <div>
                            <h3 class="text-xl font-semibold mb-2">Mystic Fortune</h3>
                            <p class="text-sm text-gray-300">96.5% RTP | High Volatility</p>
                        </div>
                    </div>
                </div>

                <div class="group relative overflow-hidden rounded-xl">
                    <img src="/api/placeholder/300/400" alt="Game 2" class="w-full h-72 object-cover transform group-hover:scale-110 transition-transform duration-300">
                    <div class="absolute inset-0 bg-gradient-to-t from-black/80 to-transparent flex items-end p-6">
                        <div>
                            <h3 class="text-xl font-semibold mb-2">Dragon's Treasure</h3>
                            <p class="text-sm text-gray-300">97.2% RTP | Medium Volatility</p>
                        </div>
                    </div>
                </div>

                <div class="group relative overflow-hidden rounded-xl">
                    <img src="/api/placeholder/300/400" alt="Game 3" class="w-full h-72 object-cover transform group-hover:scale-110 transition-transform duration-300">
                    <div class="absolute inset-0 bg-gradient-to-t from-black/80 to-transparent flex items-end p-6">
                        <div>
                            <h3 class="text-xl font-semibold mb-2">Golden Pyramid</h3>
                            <p class="text-sm text-gray-300">95.8% RTP | High Volatility</p>
                        </div>
                    </div>
                </div>

                <div class="group relative overflow-hidden rounded-xl">
                    <img src="/api/placeholder/300/400" alt="Game 4" class="w-full h-72 object-cover transform group-hover:scale-110 transition-transform duration-300">
                    <div class="absolute inset-0 bg-gradient-to-t from-black/80 to-transparent flex items-end p-6">
                        <div>
                            <h3 class="text-xl font-semibold mb-2">Space Odyssey</h3>
                            <p class="text-sm text-gray-300">96.9% RTP | Medium Volatility</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Testimonials -->
    <section id="testimonials" class="py-20 bg-gray-900">
        <div class="container mx-auto px-4">
            <h2 class="text-4xl font-bold text-center mb-16 bg-clip-text text-transparent bg-gradient-to-r from-green-400 to-blue-400">
                What Our Players Say
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
                <div class="bg-gray-800 p-6 rounded-xl">
                    <div class="flex items-center mb-4">
                        <img src="/api/placeholder/48/48" alt="User 1" class="w-12 h-12 rounded-full">
                        <div class="ml-4">
                            <h4 class="font-semibold">Sarah J.</h4>
                            <div class="flex text-yellow-400">★★★★★</div>
                        </div>
                    </div>
                    <p class="text-gray-400">"The best online slots I've ever played! The graphics are amazing and the bonuses are generous."</p>
                </div>

                <div class="bg-gray-800 p-6 rounded-xl">
                    <div class="flex items-center mb-4">
                        <img src="/api/placeholder/48/48" alt="User 2" class="w-12 h-12 rounded-full">
                        <div class="ml-4">
                            <h4 class="font-semibold">Michael R.</h4>
                            <div class="flex text-yellow-400">★★★★★</div>
                        </div>
                    </div>
                    <p class="text-gray-400">"Customer support is fantastic! They helped me resolve my issue within minutes."</p>
                </div>

                <div class="bg-gray-800 p-6 rounded-xl">
                    <div class="flex items-center mb-4">
                        <img src="/api/placeholder/48/48" alt="User 3" class="w-12 h-12 rounded-full">
                        <div class="ml-4">
                            <h4 class="font-semibold">Emily T.</h4>
                            <div class="flex text-yellow-400">★★★★★</div>
                        </div>
                    </div>
                    <p class="text-gray-400">"Won my first jackpot here! The withdrawal process was smooth and quick."</p>
                </div>
            </div>
        </div>
    </section>

    <!-- CTA Section -->
    <section class="py-20 bg-gradient-to-r from-purple-900 via-blue-900 to-purple-900">
        <div class="container mx-auto px-4 text-center">
            <h2 class="text-4xl font-bold mb-8">Ready to Start Winning?</h2>
            <p class="text-xl text-gray-300 mb-8 max-w-2xl mx-auto">
                Join thousands of players and start your winning journey today. New players get up to 500 free spins!
            </p>
            <a href="{{ route('register') }}" class="inline-block px-8 py-4 bg-gradient-to-r from-yellow-500 to-red-500 rounded-full text-xl font-semibold hover:scale-105 transform transition duration-300">
                Claim Your Bonus Now
            </a>
        </div>
    </section>

    <!-- Footer -->
    <footer class="bg-gray-900 py-12">
        <div class="container mx-auto px-4">
            <div class="grid grid-cols-1 md:grid-cols-4 gap-8 mb-8">
                <div>
                    <h3 class="text-xl font-semibold mb-4">About Us</h3>
                    <p class="text-gray-400">Lucky Spins is your premium destination for online slot gaming entertainment.</p>
                </div>
                <div>
                    <h3 class="text-xl font-semibold mb-4">Quick Links</h3>
                    <ul class="space-y-2 text-gray-400">
                        <li><a href="#" class="hover:text-yellow-400">Games</a></li>
                        <li><a href="#" class="hover:text-yellow-400">Promotions</a></li>
                        <li><a href="#" class="hover:text-yellow-400">VIP Program</a></li>
                    </ul>
                </div>
                <div>
                    <h3 class="text-xl font-semibold mb-4">Support</h3>
                </div>
            </div>
        </div>
    </footer>
</body>
</html>
