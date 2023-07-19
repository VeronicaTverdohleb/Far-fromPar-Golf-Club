package shared;

import java.util.Date;

/**
 * This class takes in the values stored for a Lesson object
 */
public class Lesson {

    private String date;
    private String time;
    private Instructor instructor;

    public Lesson(String date, String time, Instructor instructor) {
        this.date = date;
        this.time = time;
        this.instructor = instructor;
    }

    /**
     * @return date type
     */
    public String getDate() {
        return date;
    }

    /**
     *
     * @param date Date type
     */
    public void setDate(String date) {
        this.date = date;
    }

    public Instructor getInstructor(){return instructor;}

    public void setTime(String time){
        this.time = time;
    }
    public String getTime(){return time;}
}
