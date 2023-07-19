package model;

import datamodel.DataModel;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import shared.Instructor;
import shared.Lesson;

import java.nio.charset.StandardCharsets;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.when;

class ModelManagerTest {

    @Mock DataModel dataModel;
    @Mock ModelManager modelManager;
    @Mock
    Lesson lesson;
    @Mock Lesson lesson1;
    @Mock
    Instructor instructor;
    @Mock Instructor instructor1;

    @BeforeEach
    public void setup() {
        MockitoAnnotations.openMocks(this);
        modelManager=new ModelManager(dataModel);
    }


    @Test
    void getVendors_O() throws SQLException {
        Date date = new Date(2023, 1, 1);
        String time = "10.00";
        instructor = new Instructor("Joeseph");
        ArrayList<Lesson> lessons=new ArrayList<>();
        lesson = new Lesson(date, time, instructor);
        lessons.add(lesson);

        when(dataModel.getLessons(date.toString())).thenReturn(lessons);

        byte[] result=modelManager.getLessons(date.toString());

        assertNotNull(result);
    }

    @Test
    void convertLessonsIntoByte_Z() {
        ArrayList<Lesson> lessons=new ArrayList<>();

        byte[] result= modelManager.convertIntoByte(lessons);
        String expectedJsonString= "{}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);


    }
    /*
    @Test
    void convertLessonsIntoByte_O() {
        ArrayList<VendorIngredient> vendorIngredients=new ArrayList<>();
        vendor=new Vendor("Netto");
        instructor =new Ingredient("Cheese");
        lesson =new VendorIngredient(vendor, instructor,20.9);
        vendorIngredients.add(lesson);


        byte[] result= modelManager.convertVendorsIntoByte(vendorIngredients);
        String expectedJsonString= "{\"vendor\":[{\"ingredientName\":\"Cheese\",\"price\":\"20.9\",\"vendorName\":\"Netto\"}]}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);


    }

    @Test
    void convertLessonsIntoByte_M() {
        ArrayList<VendorIngredient> vendorIngredients=new ArrayList<>();
        vendor=new Vendor("Netto");
        instructor =new Ingredient("Cheese");
        lesson =new VendorIngredient(vendor, instructor,20.9);
        vendor1=new Vendor("Bilka");
        instructor1 =new Ingredient("Cheese");
        lesson1 =new VendorIngredient(vendor1, instructor1,21.0);

        vendorIngredients.add(lesson);
        vendorIngredients.add(lesson1);


        byte[] result= modelManager.convertVendorsIntoByte(vendorIngredients);
        String expectedJsonString= "{\"vendor\":[{\"ingredientName\":\"Cheese\",\"price\":\"20.9\",\"vendorName\":\"Netto\"},{\"ingredientName\":\"Cheese\",\"price\":\"21.0\",\"vendorName\":\"Bilka\"}]}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);


    }
    */
}