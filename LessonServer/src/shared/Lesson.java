package shared;

import java.util.Date;

/**
 * This class takes in the values stored for a Lesson object
 */
public class Lesson {

    private int id;
    private String date;
    private String time;
    private Instructor instructor;

    /**
     * A 4 argument constructor
     * @param id int
     * @param date String
     * @param time String
     * @param instructor Instructor
     */
    public Lesson(int id, String date, String time, Instructor instructor) {
        this.id = id;
        this.date = date;
        this.time = time;
        this.instructor = instructor;
    }

    /**
     * Method gets Date
     * @return date type
     */
    public String getDate() {
        return date;
    }

    /**
     * Method sets Date
     * @param date Date type
     */
    public void setDate(String date) {
        this.date = date;
    }

    /**
     * Method gets Instructor for the Lesson
     * @return Instructor
     */
    public Instructor getInstructor(){return instructor;}

    /**
     * Method sets Time
     * @param time string
     */
    public void setTime(String time){
        this.time = time;
    }

    /**
     * Method gets Time
     * @return String time
     */
    public String getTime(){return time;}

    /**
     * method gets Id
     * @return int Id
     */
    public int getId() {
        return id;
    }

    /**
     * Method sets Id
     * @param id int
     */
    public void setId(int id) {
        this.id = id;
    }
}
