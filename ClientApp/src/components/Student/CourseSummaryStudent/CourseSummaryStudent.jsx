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
                <td>{course.item1.name}</td>
                <td>{course.item1.description}</td>
                <td>{course.item1.durationHrs}</td>
                <td>{}</td>
                <td>
                  {" "}
                  <a href="#">Homework</a>
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
