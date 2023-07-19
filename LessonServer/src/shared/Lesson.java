package shared;

/**
 * This class takes in the values stored for a Vendor object
 */
public class Lesson {
    private String instructorName;

    public Lesson(String vendorName) {
        this.instructorName = vendorName;
    }

    /**
     *
     * @return string type
     */
    public String getVendorName() {
        return instructorName;
    }

    /**
     *
     * @param instructorName string type
     */
    public void setVendorName(String instructorName) {
        this.instructorName = instructorName;
    }
}
