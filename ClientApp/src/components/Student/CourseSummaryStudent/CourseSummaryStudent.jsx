import React from "react";
import { Table, Container, Button } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { courseSummaryStudent } from "../../../actions/studentActions";
import { Link } from "react-router-dom";
import Loader from "../../shared/Loader/Loader";

const CourseSummaryStudent = ({ match, history }) => {
  const studentId = match.params.userId;
  const dispatch = useDispatch();
  const { courses, loading } = useSelector(
    (state) => state.courseSummaryStudent
  );
  useEffect(() => {
    dispatch(courseSummaryStudent());
  }, [dispatch]);
  const goBack = () => {
    history.goBack();
  };
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
                  <td>{course.item2}</td>
                  <td>
                    {" "}
                    <Link
                      to={`/studenthomework/${studentId}/${course.item1.courseId}`}
                    >
                      Homework
                    </Link>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Container>
      )}
    </React.Fragment>
  );
};

export default CourseSummaryStudent;
