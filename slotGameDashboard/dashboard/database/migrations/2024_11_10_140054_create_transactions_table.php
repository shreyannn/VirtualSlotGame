<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up()
    {
        Schema::create('transactions', function (Blueprint $table) {
            $table->id();
            $table->foreignId('user_id')->constrained()->onDelete('cascade');
            $table->decimal('amount', 10, 2); // Amount of money spent or coins earned
            $table->enum('type', ['purchase', 'reward', 'transfer']); // Transaction type (purchase, reward, etc.)
            $table->string('payment_method')->nullable(); // Payment method for purchases
            $table->enum('status', ['pending', 'completed', 'failed']); // Status of the transaction
            $table->timestamps();
        });
    }

    public function down()
    {
        Schema::dropIfExists('transactions');
    }
};
