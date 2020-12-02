import React from "react";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Table, Container, Button } from "react-bootstrap";
import { manageCourseInstructor } from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";
import { Link } from "react-router-dom";

const ManageCourseInstructor = () => {
  const dispatch = useDispatch();
  const { courses, loading } = useSelector((state) => state.manageCourse);
  useEffect(() => {
    dispatch(manageCourseInstructor());
  }, [dispatch]);
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Container>
          <Table>
            <thead>
              <tr>
                <th>Course Name</th>
                <th>Description</th>
                <th>Duration</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {courses.map((course, index) => (
                <tr key={index}>
                  <td>{course.name}</td>
                  <td>{course.description}</td>
                  <td>{course.durationHrs}</td>
                  <td>
                    <Link to={`/courseedit/${course.courseId}`}>Edit</Link> |{" "}
                    <Link>Archive</Link>{" "}
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
          <button type="button" className="btn btn-link">
            Back
          </button>{" "}
          <Button href="/coursecreate" className="float-right mr-3">
            Create Course
          </Button>
        </Container>
      )}
    </React.Fragment>
  );
};

export default ManageCourseInstructor;
