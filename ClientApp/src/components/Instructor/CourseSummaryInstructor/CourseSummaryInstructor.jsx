import React from "react";
import { Table, Container, Button, Modal } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  getCoursesByCohortId,
  archiveAssignedCourse,
} from "../../../actions/instructorActions";

const CourseSummaryInstructor = ({ match }) => {
  const cohortId = match.params.id;
  const dispatch = useDispatch();
  const onArchive = (courseId) => {
    dispatch(archiveAssignedCourse({ courseId, cohortId }));
  };

  const { loading, courses } = useSelector(
    (state) => state.getCoursesByCohortId
  );
  const { success } = useSelector((state) => state.assignedCourseArchive);

  useEffect(() => {
    dispatch(getCoursesByCohortId(cohortId));
  }, [dispatch, success]);
  return (
    <React.Fragment>
      <Container>
        <h2>Cohort 4.2</h2>
        <Table>
          <thead>
            <tr>
              <th>Course Name</th>
              <th>Description</th>
              <th>Duration</th>
              <th>Instructor</th>
              <th>Homework</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {courses
              .filter((course) => course.item1.archive == false)
              .map((course) => (
                <tr>
                  <td>{course.item1.name}</td>
                  <td>{course.item1.description}</td>
                  <td>{course.item1.durationHrs}</td>
                  <td>{course.item2}</td>
                  <td>
                    <Link
                      to={`/instructorhomework/${cohortId}/${course.item1.courseId}`}
                    >
                      Homework
                    </Link>
                  </td>
                  <td>
                    {" "}
                    <Link
                      to={`/courseeditassigned/${cohortId}/${course.item1.courseId}`}
                    >
                      Edit
                    </Link>{" "}
                    |{" "}
                    <Link onClick={() => onArchive(course.item1.courseId)}>
                      Remove
                    </Link>{" "}
                  </td>
                </tr>
              ))}
          </tbody>
        </Table>
        <Link to="/cohortsummary">
          <button type="button" className="btn btn-link">
            Back
          </button>{" "}
        </Link>
        <Button href={`/courseassign/${cohortId}`} className="float-right mr-3">
          Add Course
        </Button>
      </Container>
    </React.Fragment>
  );
};

export default CourseSummaryInstructor;
