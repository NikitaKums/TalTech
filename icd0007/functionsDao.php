<?php
class FunctionsDao {

    private $connection;

    function __construct() {
        $this->connection = new PDO('sqlite:data.sqlite');
        $this->connection->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    }

    function inputValidation($firstName, $lastName, $phone1, $phone2, $phone3) {
        $errors = [];
        if (empty($firstName)) {
            array_push($errors, "Nimi on puudu!");
        }
        if (empty($lastName)) {
            array_push($errors, "Perekonnanimi on puudu!");
        }
        if (empty($phone1) && empty($phone2) && empty($phone3)) {
            array_push($errors, "Telefoninumber on puudu!");
        }
        if (strlen($firstName) < 2) {
            array_push($errors, "Nime pikkus ei tohi olla väiksem kui 2!");
        }
        if (strlen($lastName) < 2){
            array_push($errors, "Perekonnanime pikkus ei tohi olla väiksem kui 2!");
        }
        return $errors;//need to push
    }

    function save($firstName, $lastName, $phone1, $phone2, $phone3) {
        $phonenumbers = [$phone1, $phone2, $phone3];
        $stmt = $this->connection->prepare('INSERT INTO users (firstName, lastName)
          VALUES (:firstName, :lastName)');
        $stmt->bindValue(':firstName', $firstName);
        $stmt->bindValue(':lastName', $lastName);
        $stmt->execute();
        $id = $this->connection->lastInsertId();
        $this->savePhonenumbers($id, $phonenumbers);
    }

    private function savePhonenumbers($id, $phonenumbers) {
        $stmt = $this->connection->prepare('INSERT INTO phonenumbers (contact_id, phonenumber) 
          VALUES (:contact_id, :phonenumber)');
        foreach ($phonenumbers as $phonenumber) {
            $stmt->bindValue(':contact_id', $id);
            $stmt->bindValue(':phonenumber', $phonenumber);
            $stmt->execute();
        }
    }
    function readAll() {
        $dict = [];
        $stmt = $this->connection->prepare('SELECT * FROM users, phonenumbers WHERE id = contact_id ORDER BY id');
        $stmt->execute();
        foreach ($stmt as $row) {
            if (isset($dict[$row['id']])) {
                array_push($dict[$row['id']]->phones, $row['phonenumber']);
            } else {
                $dict[$row['id']] = new UserObject($row['firstName'], $row['lastName'], $row['phonenumber'], $row['id']);
            }
        }
        return $dict;
    }

    function editUser($id, $firstName, $lastName, $phone1, $phone2, $phone3) {
        $phonenumbers = [$phone1, $phone2, $phone3];
        $stmt = $this->connection->prepare('UPDATE users SET firstName = (:firstName), lastName = (:lastName) 
          WHERE id = (:id)');
        $stmt->bindValue(':firstName', $firstName);
        $stmt->bindValue(':lastName', $lastName);
        $stmt->bindValue(':id', $id);
        $stmt->execute();
        $stmt = $this->connection->prepare('DELETE FROM phonenumbers WHERE contact_id = (:id)');
        $stmt->bindValue(':id', $id);
        $stmt->execute();
        $this->savePhonenumbers($id, $phonenumbers);
    }
}
/*
 *
function read_data_for_edit($id){
    global $connection;
    $stmt = $connection->prepare('select * from users, phonenumbers WHERE phonenumbers.id = (:id) AND users.id = (:id)');
    $stmt->bindValue(':id', $id);
    $stmt->execute();
    foreach ($stmt as $item){
        return ['firstName' => $item['firstName'], 'lastName' => $item['lastName'], 'number1' => $item['number1'], 'number2' => $item['number2'], 'number3' => $item['number3'], 'id' => $id];
    }
    return [];
}*/