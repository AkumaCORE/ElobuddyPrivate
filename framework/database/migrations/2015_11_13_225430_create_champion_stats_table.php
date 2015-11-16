<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateChampionStatsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('champion_stats', function (Blueprint $table) {
            $table->increments('id')->index();
            $table->string('range');
            $table->string('main_type_damage');
            $table->boolean('melee');
            $table->integer('champion_id')->unsigned()->unique();
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
        Schema::drop('champion_stats');
    }
}
