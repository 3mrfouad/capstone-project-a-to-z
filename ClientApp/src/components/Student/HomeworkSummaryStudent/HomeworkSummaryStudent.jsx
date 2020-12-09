import React from "react";
import { Table, Container, Row, Col, Nav } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  homeworkSummaryStudent,
  courseSummaryStudent,
} from "../../../actions/studentActions";
import { Link } from "react-router-dom";
import { LinkContainer } from "react-router-bootstrap";

const HomeworkSummaryStudent = ({ match, history }) => {
  const studentId = match.params.studentId;
  const courseId = match.params.courseId;
  const dispatch = useDispatch();
  const { homework, loading } = useSelector(
    (state) => state.homeworkSummaryStudent
  );
  const { courses } = useSelector((state) => state.courseSummaryStudent);
  const goBack = () => {
    history.goBack();
  };
  useEffect(() => {
    dispatch(homeworkSummaryStudent(courseId));
    dispatch(courseSummaryStudent());
  }, [dispatch, courseId]);
  return (
    <React.Fragment>
      <Container>
        <Row>
          <Col xs={2}>
            <Nav defaultActiveKey="/home" className="flex-column mt-5">
              {courses.map((course, index) => (
                <LinkContainer
                  key={index}
                  to={`/studenthomework/${studentId}/${course.item1.courseId}`}
                >
                  <Nav.Link
                    key={index}
                    // href={}
                  >
                    {" "}
                    {course.item1.name}{" "}
                  </Nav.Link>
                </LinkContainer>
              ))}
            </Nav>
          </Col>
          <Col xs={10}>
            <Table>
              <thead>
                <tr>
                  <th>Homework Name</th>
                  <th>Due Date</th>
                  <th>GitHub</th>
                  <th>Category</th>
                  <th>Details</th>
                </tr>
              </thead>
              <tbody>
                {homework.map((item, index) => (
                  <tr key={index}>
                    <td>{item.title}</td>
                    <td>{item.dueDate}</td>
                    <td>{item.gitHubClassRoomLink}</td>
                    <td>{item.isAssignment ? "Assignment" : "Practice"}</td>
                    <td>
                      <Link
                        to={`/homeworkcardstudent/${studentId}/${item.homeworkId}`}
                      >
                        View
                      </Link>{" "}
                      {/*  |<a href="#">Grades</a> */}
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <button type="button" className="btn btn-link" onClick={goBack}>
              Back
            </button>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default HomeworkSummaryStudent;
