<?php
require_once 'lib/tpl.php';
require_once 'functionsDao.php';
require_once 'UserObject.php';

$cmd = param('cmd')? param('cmd') : 'list_page';
$functionsDao = new FunctionsDao();
$data = [];

if ($cmd === "list_page") {
    if (param('message') === 'success') {
        $data['$message'] = "Lisatud";
    } else if (param('message') === 'updated') {
        $data['$message'] = "Uuendatud!";
    }
    $data['$lines'] = $functionsDao->readAll();
    $data['$template'] = 'list_body.html';
    print render_template('main_body.html', $data);

} else if ($cmd === "add_page") {
    $data['$template'] = 'input_body.html';
    print render_template('main_body.html', $data);

} else if ($cmd === 'save') {
    //error check
    $errors = $functionsDao->inputValidation(param('firstName'), param('lastName'), param('phone1'),
        param('phone2'), param('phone3'));

    if (empty($errors)) {
        if (param('id')) {
            $functionsDao->editUser(param('id'), param('firstName'), param('lastName'),
                param('phone1'), param('phone2'), param('phone3'));
            header("Location: ?cmd=list_page&message=updated");
        } else {
            $functionsDao->save(param('firstName'), param('lastName'), param('phone1'),
                param('phone2'), param('phone3'));
            header("Location: ?cmd=list_page&message=success");
        }
    } else {
        $invalid_user_data = new UserObject(param('firstName'), param('lastName'), param('phone1'), param('id'));
        array_push($invalid_user_data->phones, param('phone2'), param('phone3'));
        $data['$user_data'] = $invalid_user_data;
        $data['$errors'] = $errors;
        $data['$template'] = 'input_body.html';
        print render_template('main_body.html', $data);
    }

} else if ($cmd === 'edit_page') {
    $data['$user_data'] = $functionsDao->readAll()[param('id')];
    $data['$template'] = 'input_body.html';
    print render_template('main_body.html', $data);

} else {
    throw new Error("Unknown command");
}

function param($key) {
    if (isset($_GET[$key])) {
        return $_GET[$key];
    } else if (isset($_POST[$key])) {
        return $_POST[$key];
    } else {
        return '';
    }
}