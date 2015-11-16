<?php

/*
|--------------------------------------------------------------------------
| Application Routes
|--------------------------------------------------------------------------
|
| Here is where you can register all of the routes for an application.
| It's a breeze. Simply tell Laravel the URIs it should respond to
| and give it the controller to call when that URI is requested.
|
*/

# Main routes
Route::get('/admin','PagesController@index');


#Usuario Routes

Route::group(['prefix' => 'usuario'], function()
{
    Route::get('login', ['as' => 'usuario.login', 'uses' => 'UsuarioController@login']);
    Route::get('novo', ['as' => 'usuario.create', 'uses' => 'UsuarioController@create']);
    Route::post('inserir', ['as' => 'usuario.store', 'uses' => 'UsuarioController@store']);
    Route::get('editar/{id}', ['as' => 'usuario.edit', 'uses' => 'UsuarioController@edit']);
    Route::get('requestLogin{login?}{senha?}', ['as' => 'usuario.requestLogin', 'uses' => 'UsuarioController@requestLogin']);
    Route::get('mostrar/{id}', ['as' => 'usuario.show', 'uses' => 'UsuarioController@show']);
    Route::post('atualizar/{id}', ['as' => 'usuario.update', 'uses' => 'UsuarioController@update']);
    Route::get('excluir/{id}', ['as' => 'usuario.remove', 'uses' => 'UsuarioController@remove']);
});

Route::group(['prefix' => 'champion'], function()
{
    Route::get('new', ['as' => 'champion.create', 'uses' => 'ChampionController@create']);
    Route::post('insert', ['as' => 'champion.store', 'uses' => 'ChampionController@store']);
    Route::get('edit/{id}', ['as' => 'champion.edit', 'uses' => 'ChampionController@edit']);
    Route::get('show/{id}', ['as' => 'champion.show', 'uses' => 'ChampionController@show']);
    Route::post('update/{id}', ['as' => 'champion.update', 'uses' => 'ChampionController@update']);
    Route::get('delete/{id}', ['as' => 'champion.remove', 'uses' => 'ChampionController@remove']);
});

Route::get('logar', ['as' => 'requestLogin', 'uses' => 'UsuarioController@requestLogin']);
Route::get('sair', ['as' => 'logout', 'uses' => 'UsuarioController@logout']);