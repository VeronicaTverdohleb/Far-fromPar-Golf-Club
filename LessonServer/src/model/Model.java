package model;


import datamodel.DataModel;

import java.util.Date;

/**
 * Interface for Server's ModelManager
 */
public interface Model {
    byte[] getLessons(String date);
}
