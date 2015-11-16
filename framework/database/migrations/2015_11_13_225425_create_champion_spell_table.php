<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateChampionSpellTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('champion_spells', function (Blueprint $table) {
            $table->increments('id')->index();
            $table->string('name');
            $table->string('slot');
            $table->string('type');
            $table->string('skillshotType');
            $table->integer('range');
            $table->integer('speed');
            $table->integer('cast_delay');
            $table->boolean('aa_reseter');
            $table->boolean('aa_disabler');
            $table->boolean('mv_disabler');
            $table->boolean('passive');
            $table->integer('champion_id')->unsigned();
            $table->timestamps();

            $table->foreign('champion_id')
                ->references('id')
                ->on('champions')
                ->onDelete('cascade');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::drop('champion_spells');
    }
}
