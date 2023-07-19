package datamodel;

import shared.Lesson;


import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;

/**
 * Interface implemented by model.DataModelManager
 */
public interface DataModel {
    ArrayList<Lesson> getLessons(String date) throws SQLException;
}
