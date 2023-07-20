package model;
import java.nio.charset.StandardCharsets;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import datamodel.DataModel;
import shared.Lesson;
import org.json.simple.JSONObject;

import java.sql.SQLException;
import java.util.ArrayList;

/**
 * Implements Model
 */
public class ModelManager implements Model {
    private DataModel dataModel;

    /**
     * Initializes DataModel interface
     * @param dataModel the interface implemented
     */
    public ModelManager(DataModel dataModel) {
        this.dataModel = dataModel;
    }


    /**
     * Method gets the lessons from the getLessons() method in DataModel
     * which is implemented in DataModelManager
     * @param date gets the lessons
     * @return a byte version of the lessons ArrayList
     */
    @Override
    public byte[] getLessons(String date) {
        ArrayList<Lesson> lessons = null;
        try {
            lessons = dataModel.getLessons(date);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        return convertIntoByte(lessons);
    }

    /***
     * Translates DB output to JSON string and then into byte array
     * The format of the JSON output is:
     * {"lesson": [ {"instructorName: "{Name}",
     *               "lessonDate": "{Date}",
     *               "lessonTime": "{Time}"}
     *            ]
     * }
     * @param lessons ArrayList of Lesson
     * @return a string of bytes
     */
    public byte[] convertIntoByte(ArrayList<Lesson> lessons) {
        // Creating map for "lesson": [list of lessons and their info]
        Map<String, ArrayList<Map<String, String>>> lessonInfo = new HashMap<>();

        // Creating Maps for each vendor and putting it in the above Map lessonInfo
        for (Lesson lesson : lessons) {
            Map<String, String> singleLessonInfo = new HashMap<>();
            // Putting values from DB output together with pre-agreed keys compatible with the C# side
            singleLessonInfo.put("instructorName", lesson.getInstructor().getName());
            singleLessonInfo.put("lessonDate", lesson.getDate().toString());
            singleLessonInfo.put("lessonTime", String.valueOf(lesson.getTime()));
            lessonInfo.computeIfAbsent("lesson", k -> new ArrayList<>()).add(singleLessonInfo);
        }

        // Map into JSON, so it has "" around keys and values
        JSONObject json = new JSONObject(lessonInfo);

        // Return byte array of the JSON
        return json.toString().getBytes(StandardCharsets.UTF_8);
    }


}
