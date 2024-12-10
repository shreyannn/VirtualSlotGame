<?php

use App\Http\Controllers\KhaltiPaymentController;
use App\Http\Controllers\PlayerController;
use App\Http\Controllers\ProfileController;
use App\Http\Controllers\TransactionController;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\Admin\AdminController;
use App\Http\Controllers\PlayerActivityController;
/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "web" middleware group. Make something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});

Route::middleware(['auth', 'role:admin'])->group(function () {
    Route::get('/admin/dashboard', [AdminController::class, 'showTotalUsers'])->name('admin.dashboard');
    Route::put('/admin/ban/{id}', [AdminController::class, 'banUser'])->name('admin.ban');
    Route::put('/admin/unban/{id}', [AdminController::class, 'unbanUser'])->name('admin.unban');
    Route::get('/admin/transaction-history', [TransactionController::class, 'adminTransactionHistory'])->name('admin.transaction-history');
});


Route::middleware('role:player')->group(function () {
    Route::get('/player/dashboard', [PlayerController::class, 'index'])->name('player.dashboard');
    Route::get('/buy-coins', [TransactionController::class, 'showBuyCoinsForm'])->name('buy.coins.form');
    Route::post('/buy-coins', [TransactionController::class, 'buyCoins'])->name('buy.coins');
    Route::get('/transaction-history', [TransactionController::class, 'transactionHistory'])->name('player.transaction-history');
    Route::post('/initiate-payment', [KhaltiPaymentController::class, 'initiatePayment'])->name('initiate.payment');
    Route::get('/payment-response', [KhaltiPaymentController::class, 'paymentResponse'])->name('payment.response');

    Route::get('/player-activity', [PlayerActivityController::class, 'PlayerHistory'])->name('player.player-history');
});


require __DIR__ . '/auth.php';
