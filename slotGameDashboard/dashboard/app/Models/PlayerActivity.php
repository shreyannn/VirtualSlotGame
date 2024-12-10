<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class PlayerActivity extends Model
{
    use HasFactory;

    protected $fillable = [
        'user_id',
        'old_balance',
        'new_balance',
        'spin_count',
        'bet',
        'win',
        'RTP',
    ];

    public function user()
    {
        return $this->belongsTo(User::class);
    }
} 