package shared;

/**
 * This class takes in the values stored for an Ingredient object
 */
public class Instructor {
    private int id;
    private String instructorName;
    public Instructor(String instructorName, int id) {
        this.instructorName = instructorName;
        this.id = id;
    }

    /**
     *
     * @param instructorName sets the string
     */
    public void setName(String instructorName) {
        this.instructorName = instructorName;
    }

    /**
     *
     * @return string type
     */
    public String getName() {
        return instructorName;
    }
}
