/**
 * Created by Gabriel on 01/10/2015.
 */

var UsuarioController = UsuarioController || {};

UsuarioController.common = {
    init : function()
    {
        $( "#efetuaLogin" ).click(function() {
            $.get( "/framework/public/logar", { login: $('#email').val(), senha: $('#senha').val() } )
                .done(function( data ) {
                    if(data === 'logado'){
                        window.location.href = "/framework/public/admin"
                    }else{
                        alert(data + " ói");
                    }
                });
        });
    }
}

UsuarioController.events = {

    adicionarTratador: function(elemento, tipo, funcao) {

        // codigos

    }

}