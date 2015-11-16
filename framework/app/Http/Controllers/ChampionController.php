<?php

namespace App\Http\Controllers;


use App\Champion;
use App\Http\Requests;
use App\Http\Controllers\Controller;
use App\Stats;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Redirect;
use Illuminate\Support\Facades\Request;

class ChampionController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        //
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        if (!Auth::check())
        {
            return Redirect::action('UsuarioController@login');
        }

        return view('champion.create');
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        $input = Request::all();

        $champ = new Champion();
        $champ->name = $input['name'];
        $champ->imgUrl = $input['imgUrl'];
        $champ->save();

        $stats = new Stats();

        $stats->range = $input['range'];
        $stats->main_type_damage = $input['main_type_damage'];
        $stats->melee = array_key_exists("melee", $input) ? $input['melee'] : 0;
        $stats->champion_id = $champ->id;

        $stats->save();

        dd($champ);
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function show($id)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy($id)
    {
        //
    }
}
