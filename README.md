# Operation CHAN

To get started after cloning, create new branch by running `git checkout -b NEW-BRANCH-NAME main` & `git pull origin main`

see how git works here: [github-rebase](https://www.atlassian.com/git/tutorials/rewriting-history/git-rebase)

After making changes, stage the changes by running `git add .` or `git add SPECIFIC-FILE`, and then commit and push: `git commit -m"MESSAGE"` and then `git push`

## :rocket: Branching Strategy

## `main`

- Must **always** be production ready
- Can **not** be deleted
- Merges to main must pass peer review 
- All changes to main must be trough a merge from a branch (due to the above requirement)

## Branch naming convention

- Branches should include task id in the branch name, exampel: `CHAN-1/initial-start`
- Limited lifetime:
  - **Must** be deleted once the work contained within the branch is done, and the branch is merged to `main`
- Rewriting git history is allowed (rebasing and `push --force`) as long as commits in the rebase exists *only* in the branch


## :raised_hand: Project report

Remember to update [project report](https://www.overleaf.com/project/635675ed670de6ab20ffab7b)

