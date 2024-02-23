# Description

This repository contains design, documentation and the source code for our kids arithmetic game with subtraction questions only.

# How to use this repository?
This repository is maintained as the main source of truth. Nothing should be merged to this repository directly. 
Fork the repo, make changes and submit pull requests.
Wait for review from 2 people from appropriate teams before merging. 

Some basic usage steps are described below:

1. Always fork this repository to your account.
2. Clone the forked repository from your GitHub account.
3. Create a branch and name it "azure-<Azure Ticket Number>". Put in the ticket number of the ticket you are assigned.
4. Make your changes
5. Push the changes to your forked repository.
6. Open a Pull Request and fill out the Pull Request Template for information regarding your change.
7. Wait for reviews. <b> DO NOT MERGE ANY CHANGES WITHOUT 2 REVIEWS.</b>

# Maintaining your forked Git repo:

1. Before working on any new ticket, always sync your forked repo with the main. To do that, you will need to add a remote
   1. Add a remote called `upstream` pointing to the main repos using `git remote add upstream https://github.com/dhruvikapadia096/Team2_dog_math.git`
   2. Pull changes from upstream: `git fetch upstream`
   3. Checkout main branch: `git checkout main`
   4. Rebase changes from upstream main branch to your local main branch: `git rebase upstream/main`
2. You can create and checkout a new branch using `git checkout -b azure-1`. Make sure you do the above before starting working on a new ticket.
3. Push as many changes you want but they will be to your forked repository. Once done, open a Pull Request and ask for reviews from 2 people from relevant team.
