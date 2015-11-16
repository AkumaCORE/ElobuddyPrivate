<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Stats extends Model
{
    protected $table = 'champion_stats';

    protected $fillable = ['range', 'main_type_damage', 'melee', 'champion_id'];

    public function champion()
    {
        return $this->belongsTo('App\Champion');
    }
}
