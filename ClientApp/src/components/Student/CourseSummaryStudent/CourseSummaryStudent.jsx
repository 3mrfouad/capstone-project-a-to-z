import React from "react";
import { Table, Container, Button } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { courseSummaryStudent } from "../../../actions/studentActions";

const CourseSummaryStudent = () => {
  const dispatch = useDispatch();
  const { courses, loading } = useSelector(
    (state) => state.courseSummaryStudent
  );
  useEffect(() => {
    dispatch(courseSummaryStudent());
  }, [dispatch]);
  return (
    <React.Fragment>
      <Container>
        <Table>
          <thead>
            <tr>
              <th>Course Name</th>
              <th>Description</th>
              <th>Duration</th>
              <th>Instructor</th>
              <th>Homework</th>
            </tr>
          </thead>
          <tbody>
            {courses.map((course, index) => (
              <tr key={index}>
                <td>{course.name}</td>
                <td>{course.description}</td>
                <td>{course.durationHrs}</td>
                <td>{}</td>
                <td>
                  {" "}
                  <a href="#">{course.homeworks[0]}</a>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        <button type="button" className="btn btn-link">
          Back
        </button>
      </Container>
    </React.Fragment>
  );
};

export default CourseSummaryStudent;
