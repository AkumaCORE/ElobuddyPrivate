<?php

namespace App\Http\Controllers;

use App\Usuario;
use Illuminate\Http\Request;
use App\Http\Requests;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Input;
use Illuminate\Support\Facades\Redirect;

class UsuarioController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public $debugMode = true;

    public function login()
    {
        if($this->debugMode){
            if(Usuario::all()->isEmpty()){
                $nUser = new Usuario();

                $nUser->name = "admin";
                $nUser->password = bcrypt('admin');;
                $nUser->email = "gmlyra@live.com";
                $nUser->save();
            }
            $user = Usuario::find(1);

            Auth::login($user);
            return Redirect::action('PagesController@index');
        }

        if (Auth::check())
        {
            return Redirect::action('PagesController@index');
        }
        return view('usuario.login');
    }

    public function requestLogin(Request $request)
    {
        if($this->debugMode){
            $user = Usuario::find(1);

            Auth::login($user);
            return 'logado';
        }

        $email = Input::get( 'login' );
        $senha = Input::get( 'senha' );

        $usuario = Usuario::where('email', $email)->first();

        if($usuario == null){
            return 'Usuario não encontrado';
        }elseif(!Hash::check($senha, $usuario->password)){
            return 'Senha Invalida';
        }else{
            Auth::login($usuario);
            return 'logado';
        }
    }

    public function logout()
    {
        Auth::logout();
        return Redirect::action('UsuarioController@login');
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        //
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
