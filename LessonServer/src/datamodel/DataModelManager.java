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
        return DriverManager.getConnection("jdbc:postgresql://localhost:5432/lessons_db","postgres","bobs");
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
            String query = "select * "+
                    "from lesson "+
                    "where date = "+ "'" +lessonDate+ "'";
            System.out.println(query);
            PreparedStatement preparedStatement=connection.prepareStatement(query) ;
            //System.out.println(preparedStatement);
            ResultSet resultSet=preparedStatement.executeQuery();
            ArrayList<Lesson> lessons=new ArrayList<>();
            while (resultSet.next()){
                String lessonsDate=resultSet.getString(3);
                if(lessonsDate==null){
                    throw new SQLException("No lessons for this date");
                }

                int instructorId=resultSet.getInt(2);
                PreparedStatement statement=connection.prepareStatement("select *" +
                        " from instructor" +
                        " where id = "+ instructorId);
                ResultSet resultSet1 = statement.executeQuery();
                while(resultSet1.next()) {
                    String name = resultSet1.getString(2);
                    Instructor instructor = new Instructor(name, instructorId);
                    String time = resultSet.getString(4);
                    Lesson l = new Lesson(lessonsDate, time, instructor);

                    lessons.add(l);
                }
                if (lessons.size() == 0){
                    throw new SQLException("No lessons for this date");
                }
            }
            return lessons;
        }
    }
}
