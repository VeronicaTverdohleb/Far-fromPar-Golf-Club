package shared;

/**
 * This class takes in the values stored for a VendorIngredient object
 */
public class InstructorLesson {
    private Vendor vendor;
    private Ingredient ingredient;
    private double price;

    /**
     * This is a 3 argument constructor
     * @param vendor takes in a Vendor
     * @param ingredient takes in an Ingredient
     * @param price takes in an integer
     */
    public InstructorLesson(Vendor vendor, Ingredient ingredient, double price) {
        this.vendor = vendor;
        this.ingredient = ingredient;
        this.price = price;
    }

    /**
     *
     * @return instructor type
     */
    public Ingredient getInstructor() {
        return ingredient;
    }
    /**
     *
     * @return lesson type
     */
    public Vendor getLesson() {
        return vendor;
    }
    /**
     *
     * @return int type
     */
    public double getPrice() {
        return price;
    }
}
