<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Spell extends Model
{
    protected $table = 'champion_spells';

    protected $fillable = ['name', 'slot', 'type' , 'skillshotType', 'range', 'speed', 'cast_delay', 'aa_reseter', 'aa_disabler', 'mv_disabler', 'passive' , 'champion_id'];

    public function champion()
    {
        return $this->belongsTo('App\Champion');
    }

}
