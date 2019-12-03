<?php
class UserObject{
    public $firstName;
    public $lastName;
    public $phones = [];
    public $id;
    public function __construct($firstName, $lastName, $phone, $id){
        $this->firstName = $firstName;
        $this->lastName = $lastName;
        $this->phones[] = $phone;
        $this->id = $id;
    }
}// need to push