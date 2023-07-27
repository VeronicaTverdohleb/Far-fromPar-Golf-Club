package shared;

/**
 * This class takes in the values stored for an Instructor object
 */
public class Instructor {
    private int id;
    private String instructorName;

    /**
     * Two argument constructor for Instructor
     * @param instructorName
     * @param id
     */
    public Instructor(String instructorName, int id) {
        this.instructorName = instructorName;
        this.id = id;
    }

    /**
     * Method to set the instructors name
     * @param instructorName sets the string
     */
    public void setName(String instructorName) {
        this.instructorName = instructorName;
    }

    /**
     * Method to get instructors name
     * @return string type
     */
    public String getName() {
        return instructorName;
    }
}
