import React, { useEffect } from "react";
import { Table, Container, Button, Nav, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
    getHomeworkSummaryInstructor,
    getCoursesByCohortId,
} from "../../../actions/instructorActions";
import { Link } from "react-router-dom";
import Loader from "../../shared/Loader/Loader";
import { LinkContainer } from "react-router-bootstrap";

const HomeworkSummaryInstructor = ({ match, history }) => {
    const cohortId = match.params.id;
    const courseId = match.params.courseId;
    const dispatch = useDispatch();
    useEffect(() => {
            dispatch(getHomeworkSummaryInstructor({ courseId, cohortId }));
            dispatch(getCoursesByCohortId(cohortId));
        },
        [dispatch, courseId]);

    const { loading, error, homeworkSummary } = useSelector(
        (state) => state.homeworkSummaryInstructor
    );
    const { courses } = useSelector((state) => state.getCoursesByCohortId);

    const goBack = () => {
        history.goBack();
    };

    return (
        <React.Fragment>
            {loading
                ? (
                    <Loader/>
                )
                : (
                    <Container>
                        <Row>
                            <Col xs={2}>
                                <Nav className="flex-column">
                                    {courses.map((course, index) => (
                    <LinkContainer
                    key={index}
                    to={`/instructorhomework/${cohortId}/${course.item1.courseId}`}
                  >
                    <Nav.Link
                      key={index}
                    >
                      {course.item1.name}
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
                                        <th>Release Date</th>
                                        <th>GitHub</th>
                                        <th>Category</th>
                                        <th>Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    {homeworkSummary
                      .filter((homework) => homework.archive == false)
                      .map((homework, index) => (
                          <tr key={index}>
                        <td>{homework.title}</td>
                        <td>{homework.dueDate.split("T")[0]}</td>
                        <td>{homework.releaseDate.split("T")[0]}</td>
                        <td>
                          <a target="_blank" href={homework.documentLink}>
                            GitHubLink
                          </a>
                        </td>
                        <td>
                          {homework.isAssignment ? "Assignment" : "Practice"}
                        </td>
                        <td>
                          <Link
                            to={`/gradingsummary/${homework.cohortId}/${homework.homeworkId}/${homework.courseId}`}
                          >
                            Grades{" "}
                          </Link>
                          <Link
                            to={`/homeworkviewinstructor/${homework.homeworkId}`}
                          >
                            Details/Edit
                          </Link>{" "}
                        </td>
                      </tr>
                      ))}
                                    </tbody>
                                </Table>
                                <button type="button" className="btn btn-link" onClick={goBack}>
                                    Back
                                </button>{" "}
                                <LinkContainer to={`/instructorcreatehomework/${cohortId}`}>
                                    <Button className="float-right">Create</Button>
                                </LinkContainer>
                            </Col>
                        </Row>
                    </Container>
                )}
        </React.Fragment>
    );
};

export default HomeworkSummaryInstructor;