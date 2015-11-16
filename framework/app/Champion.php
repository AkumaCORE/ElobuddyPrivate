<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Champion extends Model
{
    protected $table = 'champions';

    protected $fillable = ['name', 'imgUrl'];

    public function stats()
    {
        return $this->hasOne('App\Stats');
    }

    public function spells()
    {
        return $this->hasMany('App\Spell');
    }

}
