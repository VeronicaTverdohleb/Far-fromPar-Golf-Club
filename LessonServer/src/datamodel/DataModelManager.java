package datamodel;

import shared.Instructor;
import shared.Lesson;

import java.sql.*;
import java.util.ArrayList;
import java.util.Date;

/**
 * Implements DataModel interface
 * Connects to the database in order to retrieve vendors
 */
public class DataModelManager implements DataModel {

    /**
     * Public constructor ensuring the database driver connection
     * @throws SQLException
     */
    public DataModelManager()throws SQLException{
        DriverManager.registerDriver(new org.postgresql.Driver());
    }

    /**
     * Gets the connection with the database
     * @return Connection of the database
     * @throws SQLException
     */
    private Connection getConnection() throws SQLException{
        return DriverManager.getConnection("jdbc:postgresql://localhost:5432/vendor_db","postgres","bobs");
    }

    /**
     * Method that returns an ArrayList of all the vendors from the database
     * who have the requested ingredient
     * @param lessonDate the ingredients name which will be requested
     * @return vendor (name, ingredient, price)
     * @throws SQLException
     */
    @Override
    public ArrayList<Lesson> getLessons(String lessonDate) throws SQLException {
        try(Connection connection=getConnection()){
            PreparedStatement preparedStatement=connection.prepareStatement("select * " +
                    " from lesson" +
                    " where lessonDate = "+ "'" +lessonDate+ "'") ;
            //System.out.println(preparedStatement);
            ResultSet resultSet=preparedStatement.executeQuery();
            ArrayList<Lesson> lessons=new ArrayList<>();
            while (resultSet.next()){
                String lessonsDate=resultSet.getString(1);
                if(lessonsDate==null){
                    throw new SQLException("No lessons for this date");
                }

                //Need help solving this!
                String inName=resultSet.getString(3);
                Instructor instructor = new Instructor(inName);
                String time=resultSet.getString(2);
                Lesson l = new Lesson(new Date(lessonDate), time, instructor);

                lessons.add(l);

            }
            return lessons;
        }
    }
}
