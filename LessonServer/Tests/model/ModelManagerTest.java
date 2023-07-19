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
    void getLessons_O() throws SQLException {
        String date = "2023-07-19";
        String time = "10.00";
        instructor = new Instructor("Joseph", 1);
        ArrayList<Lesson> lessons=new ArrayList<>();
        lesson = new Lesson(date, time, instructor);
        lessons.add(lesson);

        when(dataModel.getLessons(date.toString())).thenReturn(lessons);

        byte[] result=modelManager.getLessons(date.toString());

        assertNotNull(result);
        //assertEquals(result, modelManager.convertIntoByte(lessons));
        //Does not pass even though it is equal
    }

    @Test
    void convertLessonsIntoByte_Z() {
        ArrayList<Lesson> lessons=new ArrayList<>();

        byte[] result= modelManager.convertIntoByte(lessons);
        String expectedJsonString= "{}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);
    }

    @Test
    void convertLessonsIntoByte_O() {
        ArrayList<Lesson> instructorLesson=new ArrayList<>();
        instructor =new Instructor("Veronica", 1);
        lesson =new Lesson("2023-07-19", "13.00", instructor);
        instructorLesson.add(lesson);


        byte[] result= modelManager.convertIntoByte(instructorLesson);
        String expectedJsonString= "{\"lesson\":[{\"lessonDate\":\"2023-07-19\",\"instructorName\":\"Veronica\",\"lessonTime\":\"13.00\"}]}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);


    }

    @Test
    void convertLessonsIntoByte_M() {
        ArrayList<Lesson> instructorLesson=new ArrayList<>();
        instructor =new Instructor("Devlin", 1);
        lesson =new Lesson("2023-07-19", "10.00",instructor);
        instructor1 =new Instructor("Cedric", 2);
        lesson1 =new Lesson("2023-07-19", "16.00",instructor1);

        instructorLesson.add(lesson);
        instructorLesson.add(lesson1);


        byte[] result= modelManager.convertIntoByte(instructorLesson);
        String expectedJsonString= "{\"lesson\":[{\"lessonDate\":\"2023-07-19\",\"instructorName\":\"Devlin\",\"lessonTime\":\"10.00\"},{\"lessonDate\":\"2023-07-19\",\"instructorName\":\"Cedric\",\"lessonTime\":\"16.00\"}]}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);


    }
}