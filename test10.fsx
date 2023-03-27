open System.IO
#r "nuget: FSharp.Data, Version=5.0.2"
open FSharp.Data

let checkSuitsType = File.ReadAllText("scripts/checkSuitsType.json")

type Simple =
    JsonProvider<"""
{
  "total_count": 2,
  "check_suites": [
    {
      "id": 11157990057,
      "node_id": "CS_kwDOIMpMhM8AAAACmRFqqQ",
      "head_branch": "fSharpScriptsWithoutShebang-squashed2",
      "head_sha": "86140764e7f95649a70f117067146d4a6b7c8201",
      "status": "completed",
      "conclusion": "success",
      "url": "https://api.github.com/repos/realmarv/conventions/check-suites/11157990057",
      "before": "b0d38475ff2e95f8f424c98a85efd39658d5ff15",
      "after": "86140764e7f95649a70f117067146d4a6b7c8201",
      "pull_requests": [

      ],
      "app": {
        "id": 15368,
        "slug": "github-actions",
        "node_id": "MDM6QXBwMTUzNjg=",
        "owner": {
          "login": "github",
          "id": 9919,
          "node_id": "MDEyOk9yZ2FuaXphdGlvbjk5MTk=",
          "avatar_url": "https://avatars.githubusercontent.com/u/9919?v=4",
          "gravatar_id": "",
          "url": "https://api.github.com/users/github",
          "html_url": "https://github.com/github",
          "followers_url": "https://api.github.com/users/github/followers",
          "following_url": "https://api.github.com/users/github/following{/other_user}",
          "gists_url": "https://api.github.com/users/github/gists{/gist_id}",
          "starred_url": "https://api.github.com/users/github/starred{/owner}{/repo}",
          "subscriptions_url": "https://api.github.com/users/github/subscriptions",
          "organizations_url": "https://api.github.com/users/github/orgs",
          "repos_url": "https://api.github.com/users/github/repos",
          "events_url": "https://api.github.com/users/github/events{/privacy}",
          "received_events_url": "https://api.github.com/users/github/received_events",
          "type": "Organization",
          "site_admin": false
        },
        "name": "GitHub Actions",
        "description": "Automate your workflow from idea to production",
        "external_url": "https://help.github.com/en/actions",
        "html_url": "https://github.com/apps/github-actions",
        "created_at": "2018-07-30T09:30:17Z",
        "updated_at": "2019-12-10T19:04:12Z",
        "permissions": {
          "actions": "write",
          "administration": "read",
          "checks": "write",
          "contents": "write",
          "deployments": "write",
          "discussions": "write",
          "issues": "write",
          "merge_queues": "write",
          "metadata": "read",
          "packages": "write",
          "pages": "write",
          "pull_requests": "write",
          "repository_hooks": "write",
          "repository_projects": "write",
          "security_events": "write",
          "statuses": "write",
          "vulnerability_alerts": "read"
        },
        "events": [
          "branch_protection_rule",
          "check_run",
          "check_suite",
          "create",
          "delete",
          "deployment",
          "deployment_status",
          "discussion",
          "discussion_comment",
          "fork",
          "gollum",
          "issues",
          "issue_comment",
          "label",
          "merge_group",
          "milestone",
          "page_build",
          "project",
          "project_card",
          "project_column",
          "public",
          "pull_request",
          "pull_request_review",
          "pull_request_review_comment",
          "push",
          "registry_package",
          "release",
          "repository",
          "repository_dispatch",
          "status",
          "watch",
          "workflow_dispatch",
          "workflow_run"
        ]
      },
      "created_at": "2023-02-23T12:10:48Z",
      "updated_at": "2023-02-23T12:16:40Z",
      "rerequestable": true,
      "runs_rerequestable": false,
      "latest_check_runs_count": 3,
      "check_runs_url": "https://api.github.com/repos/realmarv/conventions/check-suites/11157990057/check-runs",
      "head_commit": {
        "id": "86140764e7f95649a70f117067146d4a6b7c8201",
        "tree_id": "381222317743d7ccb6ae59ca9d4079c9f7ef882c",
        "message": "FileConventions: add HasCorrectShebang function",
        "timestamp": "2023-02-23T12:02:15Z",
        "author": {
          "name": "realmarv",
          "email": "zahratehraninasab@gmail.com"
        },
        "committer": {
          "name": "realmarv",
          "email": "zahratehraninasab@gmail.com"
        }
      },
      "repository": {
        "id": 550128772,
        "node_id": "R_kgDOIMpMhA",
        "name": "conventions",
        "full_name": "realmarv/conventions",
        "private": false,
        "owner": {
          "login": "realmarv",
          "id": 50144546,
          "node_id": "MDQ6VXNlcjUwMTQ0NTQ2",
          "avatar_url": "https://avatars.githubusercontent.com/u/50144546?v=4",
          "gravatar_id": "",
          "url": "https://api.github.com/users/realmarv",
          "html_url": "https://github.com/realmarv",
          "followers_url": "https://api.github.com/users/realmarv/followers",
          "following_url": "https://api.github.com/users/realmarv/following{/other_user}",
          "gists_url": "https://api.github.com/users/realmarv/gists{/gist_id}",
          "starred_url": "https://api.github.com/users/realmarv/starred{/owner}{/repo}",
          "subscriptions_url": "https://api.github.com/users/realmarv/subscriptions",
          "organizations_url": "https://api.github.com/users/realmarv/orgs",
          "repos_url": "https://api.github.com/users/realmarv/repos",
          "events_url": "https://api.github.com/users/realmarv/events{/privacy}",
          "received_events_url": "https://api.github.com/users/realmarv/received_events",
          "type": "User",
          "site_admin": false
        },
        "html_url": "https://github.com/realmarv/conventions",
        "description": null,
        "fork": true,
        "url": "https://api.github.com/repos/realmarv/conventions",
        "forks_url": "https://api.github.com/repos/realmarv/conventions/forks",
        "keys_url": "https://api.github.com/repos/realmarv/conventions/keys{/key_id}",
        "collaborators_url": "https://api.github.com/repos/realmarv/conventions/collaborators{/collaborator}",
        "teams_url": "https://api.github.com/repos/realmarv/conventions/teams",
        "hooks_url": "https://api.github.com/repos/realmarv/conventions/hooks",
        "issue_events_url": "https://api.github.com/repos/realmarv/conventions/issues/events{/number}",
        "events_url": "https://api.github.com/repos/realmarv/conventions/events",
        "assignees_url": "https://api.github.com/repos/realmarv/conventions/assignees{/user}",
        "branches_url": "https://api.github.com/repos/realmarv/conventions/branches{/branch}",
        "tags_url": "https://api.github.com/repos/realmarv/conventions/tags",
        "blobs_url": "https://api.github.com/repos/realmarv/conventions/git/blobs{/sha}",
        "git_tags_url": "https://api.github.com/repos/realmarv/conventions/git/tags{/sha}",
        "git_refs_url": "https://api.github.com/repos/realmarv/conventions/git/refs{/sha}",
        "trees_url": "https://api.github.com/repos/realmarv/conventions/git/trees{/sha}",
        "statuses_url": "https://api.github.com/repos/realmarv/conventions/statuses/{sha}",
        "languages_url": "https://api.github.com/repos/realmarv/conventions/languages",
        "stargazers_url": "https://api.github.com/repos/realmarv/conventions/stargazers",
        "contributors_url": "https://api.github.com/repos/realmarv/conventions/contributors",
        "subscribers_url": "https://api.github.com/repos/realmarv/conventions/subscribers",
        "subscription_url": "https://api.github.com/repos/realmarv/conventions/subscription",
        "commits_url": "https://api.github.com/repos/realmarv/conventions/commits{/sha}",
        "git_commits_url": "https://api.github.com/repos/realmarv/conventions/git/commits{/sha}",
        "comments_url": "https://api.github.com/repos/realmarv/conventions/comments{/number}",
        "issue_comment_url": "https://api.github.com/repos/realmarv/conventions/issues/comments{/number}",
        "contents_url": "https://api.github.com/repos/realmarv/conventions/contents/{+path}",
        "compare_url": "https://api.github.com/repos/realmarv/conventions/compare/{base}...{head}",
        "merges_url": "https://api.github.com/repos/realmarv/conventions/merges",
        "archive_url": "https://api.github.com/repos/realmarv/conventions/{archive_format}{/ref}",
        "downloads_url": "https://api.github.com/repos/realmarv/conventions/downloads",
        "issues_url": "https://api.github.com/repos/realmarv/conventions/issues{/number}",
        "pulls_url": "https://api.github.com/repos/realmarv/conventions/pulls{/number}",
        "milestones_url": "https://api.github.com/repos/realmarv/conventions/milestones{/number}",
        "notifications_url": "https://api.github.com/repos/realmarv/conventions/notifications{?since,all,participating}",
        "labels_url": "https://api.github.com/repos/realmarv/conventions/labels{/name}",
        "releases_url": "https://api.github.com/repos/realmarv/conventions/releases{/id}",
        "deployments_url": "https://api.github.com/repos/realmarv/conventions/deployments"
      }
    },
    {
      "id": 11158210887,
      "node_id": "CS_kwDOIMpMhM8AAAACmRTJRw",
      "head_branch": "fSharpScriptsWithoutShebang-squashed2",
      "head_sha": "86140764e7f95649a70f117067146d4a6b7c8201",
      "status": "completed",
      "conclusion": "success",
      "url": "https://api.github.com/repos/realmarv/conventions/check-suites/11158210887",
      "before": "b0d38475ff2e95f8f424c98a85efd39658d5ff15",
      "after": "86140764e7f95649a70f117067146d4a6b7c8201",
      "pull_requests": [

      ],
      "app": {
        "id": 15368,
        "slug": "github-actions",
        "node_id": "MDM6QXBwMTUzNjg=",
        "owner": {
          "login": "github",
          "id": 9919,
          "node_id": "MDEyOk9yZ2FuaXphdGlvbjk5MTk=",
          "avatar_url": "https://avatars.githubusercontent.com/u/9919?v=4",
          "gravatar_id": "",
          "url": "https://api.github.com/users/github",
          "html_url": "https://github.com/github",
          "followers_url": "https://api.github.com/users/github/followers",
          "following_url": "https://api.github.com/users/github/following{/other_user}",
          "gists_url": "https://api.github.com/users/github/gists{/gist_id}",
          "starred_url": "https://api.github.com/users/github/starred{/owner}{/repo}",
          "subscriptions_url": "https://api.github.com/users/github/subscriptions",
          "organizations_url": "https://api.github.com/users/github/orgs",
          "repos_url": "https://api.github.com/users/github/repos",
          "events_url": "https://api.github.com/users/github/events{/privacy}",
          "received_events_url": "https://api.github.com/users/github/received_events",
          "type": "Organization",
          "site_admin": false
        },
        "name": "GitHub Actions",
        "description": "Automate your workflow from idea to production",
        "external_url": "https://help.github.com/en/actions",
        "html_url": "https://github.com/apps/github-actions",
        "created_at": "2018-07-30T09:30:17Z",
        "updated_at": "2019-12-10T19:04:12Z",
        "permissions": {
          "actions": "write",
          "administration": "read",
          "checks": "write",
          "contents": "write",
          "deployments": "write",
          "discussions": "write",
          "issues": "write",
          "merge_queues": "write",
          "metadata": "read",
          "packages": "write",
          "pages": "write",
          "pull_requests": "write",
          "repository_hooks": "write",
          "repository_projects": "write",
          "security_events": "write",
          "statuses": "write",
          "vulnerability_alerts": "read"
        },
        "events": [
          "branch_protection_rule",
          "check_run",
          "check_suite",
          "create",
          "delete",
          "deployment",
          "deployment_status",
          "discussion",
          "discussion_comment",
          "fork",
          "gollum",
          "issues",
          "issue_comment",
          "label",
          "merge_group",
          "milestone",
          "page_build",
          "project",
          "project_card",
          "project_column",
          "public",
          "pull_request",
          "pull_request_review",
          "pull_request_review_comment",
          "push",
          "registry_package",
          "release",
          "repository",
          "repository_dispatch",
          "status",
          "watch",
          "workflow_dispatch",
          "workflow_run"
        ]
      },
      "created_at": "2023-02-23T12:21:33Z",
      "updated_at": "2023-02-23T12:26:13Z",
      "rerequestable": true,
      "runs_rerequestable": false,
      "latest_check_runs_count": 3,
      "check_runs_url": "https://api.github.com/repos/realmarv/conventions/check-suites/11158210887/check-runs",
      "head_commit": {
        "id": "86140764e7f95649a70f117067146d4a6b7c8201",
        "tree_id": "381222317743d7ccb6ae59ca9d4079c9f7ef882c",
        "message": "FileConventions: add HasCorrectShebang function",
        "timestamp": "2023-02-23T12:02:15Z",
        "author": {
          "name": "realmarv",
          "email": "zahratehraninasab@gmail.com"
        },
        "committer": {
          "name": "realmarv",
          "email": "zahratehraninasab@gmail.com"
        }
      },
      "repository": {
        "id": 550128772,
        "node_id": "R_kgDOIMpMhA",
        "name": "conventions",
        "full_name": "realmarv/conventions",
        "private": false,
        "owner": {
          "login": "realmarv",
          "id": 50144546,
          "node_id": "MDQ6VXNlcjUwMTQ0NTQ2",
          "avatar_url": "https://avatars.githubusercontent.com/u/50144546?v=4",
          "gravatar_id": "",
          "url": "https://api.github.com/users/realmarv",
          "html_url": "https://github.com/realmarv",
          "followers_url": "https://api.github.com/users/realmarv/followers",
          "following_url": "https://api.github.com/users/realmarv/following{/other_user}",
          "gists_url": "https://api.github.com/users/realmarv/gists{/gist_id}",
          "starred_url": "https://api.github.com/users/realmarv/starred{/owner}{/repo}",
          "subscriptions_url": "https://api.github.com/users/realmarv/subscriptions",
          "organizations_url": "https://api.github.com/users/realmarv/orgs",
          "repos_url": "https://api.github.com/users/realmarv/repos",
          "events_url": "https://api.github.com/users/realmarv/events{/privacy}",
          "received_events_url": "https://api.github.com/users/realmarv/received_events",
          "type": "User",
          "site_admin": false
        },
        "html_url": "https://github.com/realmarv/conventions",
        "description": null,
        "fork": true,
        "url": "https://api.github.com/repos/realmarv/conventions",
        "forks_url": "https://api.github.com/repos/realmarv/conventions/forks",
        "keys_url": "https://api.github.com/repos/realmarv/conventions/keys{/key_id}",
        "collaborators_url": "https://api.github.com/repos/realmarv/conventions/collaborators{/collaborator}",
        "teams_url": "https://api.github.com/repos/realmarv/conventions/teams",
        "hooks_url": "https://api.github.com/repos/realmarv/conventions/hooks",
        "issue_events_url": "https://api.github.com/repos/realmarv/conventions/issues/events{/number}",
        "events_url": "https://api.github.com/repos/realmarv/conventions/events",
        "assignees_url": "https://api.github.com/repos/realmarv/conventions/assignees{/user}",
        "branches_url": "https://api.github.com/repos/realmarv/conventions/branches{/branch}",
        "tags_url": "https://api.github.com/repos/realmarv/conventions/tags",
        "blobs_url": "https://api.github.com/repos/realmarv/conventions/git/blobs{/sha}",
        "git_tags_url": "https://api.github.com/repos/realmarv/conventions/git/tags{/sha}",
        "git_refs_url": "https://api.github.com/repos/realmarv/conventions/git/refs{/sha}",
        "trees_url": "https://api.github.com/repos/realmarv/conventions/git/trees{/sha}",
        "statuses_url": "https://api.github.com/repos/realmarv/conventions/statuses/{sha}",
        "languages_url": "https://api.github.com/repos/realmarv/conventions/languages",
        "stargazers_url": "https://api.github.com/repos/realmarv/conventions/stargazers",
        "contributors_url": "https://api.github.com/repos/realmarv/conventions/contributors",
        "subscribers_url": "https://api.github.com/repos/realmarv/conventions/subscribers",
        "subscription_url": "https://api.github.com/repos/realmarv/conventions/subscription",
        "commits_url": "https://api.github.com/repos/realmarv/conventions/commits{/sha}",
        "git_commits_url": "https://api.github.com/repos/realmarv/conventions/git/commits{/sha}",
        "comments_url": "https://api.github.com/repos/realmarv/conventions/comments{/number}",
        "issue_comment_url": "https://api.github.com/repos/realmarv/conventions/issues/comments{/number}",
        "contents_url": "https://api.github.com/repos/realmarv/conventions/contents/{+path}",
        "compare_url": "https://api.github.com/repos/realmarv/conventions/compare/{base}...{head}",
        "merges_url": "https://api.github.com/repos/realmarv/conventions/merges",
        "archive_url": "https://api.github.com/repos/realmarv/conventions/{archive_format}{/ref}",
        "downloads_url": "https://api.github.com/repos/realmarv/conventions/downloads",
        "issues_url": "https://api.github.com/repos/realmarv/conventions/issues{/number}",
        "pulls_url": "https://api.github.com/repos/realmarv/conventions/pulls{/number}",
        "milestones_url": "https://api.github.com/repos/realmarv/conventions/milestones{/number}",
        "notifications_url": "https://api.github.com/repos/realmarv/conventions/notifications{?since,all,participating}",
        "labels_url": "https://api.github.com/repos/realmarv/conventions/labels{/name}",
        "releases_url": "https://api.github.com/repos/realmarv/conventions/releases{/id}",
        "deployments_url": "https://api.github.com/repos/realmarv/conventions/deployments"
      }
    }
  ]
}
""">

let sample = File.ReadAllText("scripts/sample.json")
let value = Simple.Parse sample

printfn "value: %A" (Some value.CheckSuites.[0])
