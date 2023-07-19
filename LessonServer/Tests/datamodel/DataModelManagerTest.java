package datamodel;

import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.mockito.junit.jupiter.MockitoExtension;
import shared.Instructor;
import shared.Lesson;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;

import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class DataModelManagerTest {
    @Mock DataModelManager dataModelManager;
    @Mock DataModel dataModel;
    @Mock
    Date date;

    @BeforeEach public void setup() throws SQLException {
        MockitoAnnotations.openMocks(this);
        dataModelManager=new DataModelManager();
    }

    @Test public void testGettingLessons_Z() throws SQLException {

        Date date = new Date(2023, 07,19);
        ArrayList<Lesson> lessons=new ArrayList<>();

        try {
            when(dataModel.getLessons(date.toString())).thenReturn(lessons);
        } catch (Exception e) {
            e.printStackTrace();
        }
        ArrayList<Lesson> actualResult= dataModel.getLessons(date.toString());

        assertEquals(lessons,actualResult);
        assertThrows(Exception.class, ()->{ throw new Exception("No lessons");});

    }

    @Test public void testGettingLessons_O() throws SQLException {
        Instructor instructor = new Instructor("Cedric", 2);
        Lesson l=new Lesson(date.toString(),"10.00",instructor);
        ArrayList<Lesson> larray=new ArrayList<>();
        larray.add(l);
        ArrayList<Lesson> larray2=new ArrayList<>();
        larray2.add(l);

        try {
            when(dataModel.getLessons(date.toString())).thenReturn(larray);
        } catch (Exception e) {
            e.printStackTrace();
        }
        ArrayList<Lesson> actualResult= dataModel.getLessons(date.toString());

        assertEquals(larray2,actualResult);
    }

    @Test public void testGettingLessons_M() throws SQLException {

        Instructor i1 = new Instructor("Thomas", 1);
        Instructor i2 = new Instructor("Joseph", 2);
        Lesson l1 = new Lesson(date.toString(), "8.00", i1);
        Lesson l2 = new Lesson(date.toString(), "14.00", i2);

        ArrayList<Lesson> list1=new ArrayList<>();
        list1.add(l1);
        list1.add(l2);
        ArrayList<Lesson> list2=new ArrayList<>();
        list2.add(l1);

        try {
            when(dataModel.getLessons(date.toString())).thenReturn(list1);
        } catch (Exception e) {
            e.printStackTrace();
        }
        ArrayList<Lesson> actualResult= dataModel.getLessons(date.toString());

        assertEquals(list1,actualResult);
    }
}