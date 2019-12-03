package tdd.greeting;

import java.util.ArrayList;
import java.util.List;

class Greeter {

    public String greet(Object name) {
        String hello = "Hello";
        String suffix = ".";
        String comma = ",";

        if (name == null){
            return "Hello, my friend.";
        }

        if (isArray(name)) {
            String[] tempArray = (String[])name;
            String uppercaseName = null;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.append(hello);
            stringBuilder.append(comma);
            stringBuilder.append(" ");
            List<String> tempList = new ArrayList<>();
            for (String tempArrayName : tempArray){
                if (isUpperCase(tempArrayName)){
                    uppercaseName = tempArrayName;
                }
                else {
                    if (nameContainsIntentionalComma(tempArrayName)){
                        tempList.add(tempArrayName.replaceAll("\"", ""));
                    }
                    else if (tempArrayName.contains(",")){
                         tempList.add(tempArrayName.split(",")[0].trim());
                         tempList.add(tempArrayName.split(",")[1].trim());
                    }
                    else {
                        tempList.add(tempArrayName);
                    }
                }
            }

            tempList.remove(uppercaseName);
            String[] nameArray = tempList.toArray(new String[tempList.size()]);

            if (nameArray.length == 2){
                String twoNameArrayGreeting = hello + comma + " " + nameArray[0] + " and " + nameArray[1] + ".";

                if (uppercaseName == null){
                    return twoNameArrayGreeting;
                }
                return twoNameArrayGreeting + " AND HELLO " + uppercaseName + "!";
            }


            for (int i = 0; i < nameArray.length; i++) {
                if (i == nameArray.length - 1) {
                    stringBuilder.append("and ");
                    stringBuilder.append(nameArray[i]);
                    stringBuilder.append(suffix);
                } else {
                    stringBuilder.append(nameArray[i]);
                    stringBuilder.append(comma);
                    stringBuilder.append(" ");
                }
            }

            return stringBuilder.toString();
        }

        if (isString(name)) {
            String nameString = (String)name;

            if (nameString.equals(nameString.toUpperCase())) {
                hello = hello.toUpperCase();
                suffix = "!";
            }
            return String.format("%s, %s%s", hello, nameString, suffix);
        }

        return null;
    }

    private boolean nameContainsIntentionalComma(String name){
        return name.startsWith("\"") && name.endsWith("\"");
    }

    private boolean isUpperCase(String name){
        return name.equals(name.toUpperCase());
    }

    private boolean isArray(Object object) {
        return object.getClass().isArray();
    }

    private boolean isString(Object object) {
        return  object.getClass().equals(String.class);
    }
}