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
        Schema::create('player_activities', function (Blueprint $table) {
            $table->id();
            $table->foreignId('user_id')->constrained()->onDelete('cascade');
            $table->decimal('old_balance', 10, 2)->default(0.00); // Balance of the in-game currency
            $table->decimal('new_balance', 10, 2)->default(0.00);
            $table->integer('spin_count')->default(0);
            $table->decimal('bet', 10, 2)->default(0.00);
            $table->decimal('win', 10, 2)->default(0.00);
            $table->decimal('RTP', 10, 2)->default(0.00);
            $table->timestamps();
        });
    }

    public function down()
    {
        Schema::dropIfExists('player_activities');
    }
};
