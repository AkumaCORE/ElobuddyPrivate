@extends('app')

@section('conteudo')

    <div id="basic-form" class="section">
        <div class="row">
            <div class="col s12 m12 l12">
                <div class="card-panel">
                    <h4 class="header2">Champion Creation</h4>
                    <div class="row">
                        @if(\Illuminate\Support\Facades\Session::has('errors'))
                            <div class="alert alert-danger">
                                {!! $errors->first() !!}
                            </div>
                        @endif
                        {!! Form::open(['route' => 'champion.store']) !!}
                            <div class="row">
                                <div class="input-field col s12">
                                    {!! Form::label('name', 'Name', array('class' => 'control-label')) !!}
                                    {!! Form::text('name', null, ['class' => 'form-control', 'id' => 'name']) !!}
                                </div>
                            </div>

                            <div class="row">
                                <div class="input-field col s12">
                                    {!! Form::label('imgUrl', 'Image Champion Link', array('class' => 'control-label')) !!}
                                    {!! Form::text('imgUrl', null, ['class' => 'form-control', 'id' => 'imgUrl']) !!}
                                </div>
                            </div>

                            <div class="row">
                                <div class="input-field col s12">
                                    {!! Form::label('range', 'Champion Range', array('class' => 'control-label')) !!}
                                    {!! Form::text('range', null, ['class' => 'form-control', 'id' => 'range']) !!}
                                </div>
                            </div>

                            <div class="row">
                                <div class="input-field col s12">
                                    {!! Form::label('main_type_damage', ' ', array('class' => 'control-label')) !!}
                                    {!! Form::select('main_type_damage', array('Physical' => 'Physical', 'Magical' => 'Magical', 'True' => 'True Damage'), 'Physical', ['class' => 'form-control', 'id' => 'main_type_damage']) !!}
                                </div>
                            </div>

                            <div class="row">
                                <div class="col s12 m8 l9">
                                    <p>
                                        <input type="checkbox" id="melee" name="melee" value="1">
                                        <label for="melee">Is melee</label>
                                    </p>
                                </div>
                            </div>

                            <button class="btn btn-primary" type="submit">Save</button>

                        {!! Form::close() !!}
                    </div>
                </div>
            </div>
        </div>
    </div>

@stop