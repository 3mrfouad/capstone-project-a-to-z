import React from "react";
import { Table, Container, Button } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { homeworkSummaryInstructor } from "../../../actions/instructorActions";

const HomeworkSummaryInstructor = () => {
  const dispatch = useDispatch();
  useEffect(() => {
    // get cohort by id
    // populate the cohort data in here
  }, []);
  const homeworkSummaryInstructor = useSelector(
    (state) => state.homeworkSummaryInstructor
  );
  const { loading, error, homeworkSummary } = homeworkSummaryInstructor;

  return (
    <React.Fragment>
      <Container>
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
            <tr>
              <td>React101</td>
              <td>2020-11-19</td>
              <td>2020-11-29</td>
              <td>
                {" "}
                <a href="#">GitHubLink</a>{" "}
              </td>
              <td>Practice</td>
              <td>Grades | Details | Edit | Archive</td>
            </tr>
            <tr>
              <td>React102</td>
              <td>2020-11-29</td>
              <td>2020-12-19</td>
              <td>
                <a href="#">GitHubLink</a>
              </td>
              <td>Assignment</td>
              <td>Grades | Details | Edit | Archive</td>
            </tr>
          </tbody>
        </Table>
        <Button>Back</Button> <Button>Create</Button>
      </Container>
    </React.Fragment>
  );
};

export default HomeworkSummaryInstructor;
