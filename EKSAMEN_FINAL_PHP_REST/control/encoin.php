<?php
/**
 * Created by PhpStorm.
 * User: Tas
 * Date: 08-01-2018
 * Time: 11:47
 */


//twig
require_once '../vendor/autoload.php';
Twig_Autoloader::register();
$loader = new Twig_Loader_Filesystem('../view');    //tells where the template is located
$twig = new Twig_Environment($loader, array(
    'auto_reload' => true
));




$template = $twig->loadTemplate('encoin.html.twig');    //Refers to 'allecoins.html.twig so we can use the template
$id = $_REQUEST['ID'];  // Parameter we take from our index page
$uri = "http://eksamenrest.azurewebsites.net/Service1.svc/coins/".$id;    // Uri for our GetOneCoin service where we also add id
$json = file_get_contents($uri);    //Puts the content of the request into $json string
$mont = json_decode($json); //Decodes the content so its readable
$twigContent = array("Mont" => $mont); // Laver et array kaldet "Mont" og benytter min variable $mont
echo $template->render($twigContent);