<!DOCTYPE html>
<html>
<head>
    <link href="//fonts.googleapis.com/css?family=Lato:100" rel="stylesheet" type="text/css">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="msapplication-tap-highlight" content="no">
    <meta name="description" content="Materialize is a Material Design Admin Template,It's modern, responsive and based on Material Design by Google. ">
    <meta name="keywords" content="materialize, admin template, dashboard template, flat admin template, responsive admin template,">
    <title>Login</title>
    <!-- Favicons-->
    <link rel="icon" href="/framework/public/images/favicon/favicon-32x32.png" sizes="32x32">
    <!-- Favicons-->
    <link rel="apple-touch-icon-precomposed" href="/framework/public/images/favicon/apple-touch-icon-152x152.png">
    <!-- For iPhone -->
    <meta name="msapplication-TileColor" content="#00bcd4">
    <meta name="msapplication-TileImage" content="/framework/public/images/favicon/mstile-144x144.png">
    <!-- For Windows Phone -->


    <!-- CORE CSS-->

    <link href="/framework/public/css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection">
    <link href="/framework/public/css/style.css" type="text/css" rel="stylesheet" media="screen,projection">
    <!-- Custome CSS-->
    <link href="/framework/public/css/custom-style.css" type="text/css" rel="stylesheet" media="screen,projection">
    <!-- CSS for full screen (Layout-2)-->
    <link href="/framework/public/css/style-fullscreen.css" type="text/css" rel="stylesheet" media="screen,projection">
    <link href="/framework/public/css/page-center.css" type="text/css" rel="stylesheet" media="screen,projection">

    <!-- INCLUDED PLUGIN CSS ON THIS PAGE -->
    <link href="/framework/public/css/prism.css" type="text/css" rel="stylesheet" media="screen,projection">
    <link href="/framework/public/js/plugins/perfect-scrollbar/perfect-scrollbar.css" type="text/css" rel="stylesheet" media="screen,projection">

</head>

<body class="cyan">
<!-- Start Page Loading -->
<div id="loader-wrapper">
    <div id="loader"></div>
    <div class="loader-section section-left"></div>
    <div class="loader-section section-right"></div>
</div>
<!-- End Page Loading -->



<div id="login-page" class="row">
    <div class="col s12 z-depth-4 card-panel">
        <form class="login-form">
            <div class="row">
                <div class="input-field col s12 center">
                    <img src="/framework/public/images/login-logo.png" alt="" class="circle responsive-img valign profile-image-login">
                    <p class="center login-form-text">Gmlyra Framework</p>
                </div>
            </div>
            <div class="row margin">
                <div class="input-field col s12">
                    <i class="mdi-social-person-outline prefix"></i>
                    <input id="email" type="text">
                    <label for="email" class="center-align">E-Mail</label>
                </div>
            </div>
            <div class="row margin">
                <div class="input-field col s12">
                    <i class="mdi-action-lock-outline prefix"></i>
                    <input id="senha" type="password">
                    <label for="senha">Senha</label>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s12">
                    <a href="#" id="efetuaLogin" class="btn waves-effect waves-light col s12">Logar</a>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s6 m6 l6">
                    <p class="margin medium-small"><a href="page-register.html">Registrar</a></p>
                </div>
                <div class="input-field col s6 m6 l6">
                    <p class="margin right-align medium-small"><a href="page-forgot-password.html">Esqueci a senha</a></p>
                </div>
            </div>

        </form>
    </div>
</div>



<!-- ================================================
  Scripts
  ================================================ -->

<!-- jQuery Library -->
<script type="text/javascript" src="/framework/public/js/jquery-1.11.2.js"></script>
<!--materialize js-->
<script type="text/javascript" src="/framework/public/js/materialize.js"></script>
<!--prism-->
<script type="text/javascript" src="/framework/public/js/prism.js"></script>
<!--scrollbar-->
<script type="text/javascript" src="/framework/public/js/plugins/perfect-scrollbar/perfect-scrollbar.js"></script>

<!--plugins.js - Some Specific JS codes for Plugin Settings-->
<script type="text/javascript" src="/framework/public/js/plugins.js"></script>

<!-- JS Controllers -->

<script type="text/javascript" src="/framework/public/js/controllers/UsuarioController.js"></script>

<script>
    UsuarioController.common.init();
</script>

</body>

</html>